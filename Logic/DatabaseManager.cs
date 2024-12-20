using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Logic
{
    public class DatabaseManager
    {
        private OleDbConnection mc;
        public DatabaseManager()
        {
            mc = new OleDbConnection(DBConfig.ConnectionString);
            mc.Open();
        }
        public void CreateTable(string name, IEnumerable<TableColumn> columns, string connectToTable = null)
        {
            // Create a new table
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"CREATE TABLE [{name}] (");
            queryBuilder.AppendLine(string.Join(",\n", columns));
            queryBuilder.AppendLine(");");
            string query = queryBuilder.ToString();
            OleDbCommand command = new OleDbCommand(query, mc);
            command.ExecuteNonQuery();

            // Check if we should connect it to another table
            if (string.IsNullOrEmpty(connectToTable)) return;

            // Connecting to another table
            // 1. Find primary key in new table
            TableColumn primaryColumn = columns.Where(c => c.Key).First();
            string primaryKey = primaryColumn.Name;
            TableColumnType primaryColumnType = primaryColumn.Type;
            string resultType = primaryColumnType == TableColumnType.Int ? "INTEGER" : "TEXT(255)";

            // 2. Create a new column
            string newColumn = $"{name}_ID";
            string addColumnQuery =
                $"ALTER TABLE[{connectToTable}]\n" +
                $"ADD COLUMN[{newColumn}] {resultType};";
            OleDbCommand addColumnCommand = new OleDbCommand(addColumnQuery, mc);
            addColumnCommand.ExecuteNonQuery();


            // 3. Create a new relation
            Relation relation = new Relation()
            {
                pkTableName = name,
                pkColumnName = primaryKey,
                fkTableName = connectToTable,
                fkColumnName = newColumn,
                fkName = $"{name}{connectToTable}"
            };

            string relationQuery =
                $"ALTER TABLE [{relation.fkTableName}]\n" +
                $"ADD CONSTRAINT [{relation.fkName}]\n" +
                $"FOREIGN KEY ([{relation.fkColumnName}])\n" +
                $"REFERENCES [{relation.pkTableName}] ([{relation.pkColumnName}]);";
            OleDbCommand relationCommand = new OleDbCommand(relationQuery, mc);
            relationCommand.ExecuteNonQuery();
        }
        public void DeleteTable(string name)
        {
            List<Relation> relations = GetRelations()
                .Where(r => r.pkTableName == name || r.fkTableName == name)
                .ToList();
            foreach(Relation relation in relations)
            {
                string dropConstraintQuery = $"ALTER TABLE [{relation.fkTableName}] DROP CONSTRAINT [{relation.fkName}];";
                OleDbCommand dropConstraintCommand = new OleDbCommand(dropConstraintQuery, mc);
                dropConstraintCommand.ExecuteNonQuery();

                string dropColumnQuery = $"ALTER TABLE [{relation.fkTableName}] DROP COLUMN [{relation.fkColumnName}];";
                OleDbCommand dropColumnCommand = new OleDbCommand(dropColumnQuery, mc);
                dropColumnCommand.ExecuteNonQuery();
            }
            string query = $"DROP TABLE [{name}];";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.ExecuteNonQuery();
        }
        public DataTable GetTable(string name)
        {
            DataTable table = new DataTable();
            string query = $"SELECT * FROM [{name}];";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, mc);
            adapter.Fill(table);
            return table;
        }
        public void BackupDatabase()
        {
            string pathToDatabase = DBConfig.PathToFolder + @"\db.mdb";
            string time = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string pathToDatabaseBackup = DBConfig.PathToFolder + $@"\db-backup_{time}.mdb";
            System.IO.File.Copy(pathToDatabase, pathToDatabaseBackup);
        }

        public Dictionary<string, List<string>> GetTables()
        {
            Dictionary<string, List<string>> tables = new Dictionary<string, List<string>>();

            DataTable schemaTables = mc.GetSchema("Tables");
            DataTable schemaColumns = mc.GetSchema("Columns");

            foreach (DataRow row in schemaTables.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                string tableType = row["TABLE_TYPE"].ToString();

                // Filter out system tables
                if (tableType == "TABLE")
                {
                    tables[tableName] = new List<string>();
                }
            }

            // schemaColumns.Rows has the following info:
            //
            // TABLE_NAME
            // COLUMN_NAME
            // DATA_TYPE
            // IS_NULLABLE
            // COLUMN_HASDEFAULT
            // COLUMN_DEFAULT
            // ORDINAL_POSITION
            foreach (DataRow row in schemaColumns.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                string columnName = row["COLUMN_NAME"].ToString();

                if (tables.ContainsKey(tableName))
                {
                    tables[tableName].Add(columnName);
                }
            }

            return tables;
        }

        public Dictionary<string, string> GetPrimaryKeys()
        {
            Dictionary<string, List<string>> tableColumns = GetTables();
            Dictionary<string, string> primaryKeys = new Dictionary<string, string>();
            DataTable schemaIndexes = mc.GetSchema("Indexes");

            // Iterate over indexes to find relationships (Foreign Keys are usually constraints in indexes)
            // schemaIndexes.Rows has the following info:
            //
            // TABLE_NAME
            // COLUMN_NAME
            // INDEX_NAME
            // PRIMARY_KEY
            // UNIQUE
            // NULLS
            foreach (DataRow row in schemaIndexes.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();   // Table containing the index
                string columnName = row["COLUMN_NAME"].ToString(); // Column part of the index
                bool isPrimaryKey = row["PRIMARY_KEY"].ToString() == "True"; // Whether this is part of a primary key

                if (!tableColumns.ContainsKey(tableName))
                    continue;

                if (!isPrimaryKey)
                    continue;

                primaryKeys[tableName] = columnName;
            }
            return primaryKeys;
        }

        public IEnumerable<Relation> GetRelations()
        {
            List<Relation> relations = new List<Relation>();

            DataTable foreignKeys = mc.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
            foreach (DataRow row in foreignKeys.Rows)
            {
                string fkTableName = row["FK_TABLE_NAME"].ToString(); // Таблица с внешним ключом
                string fkColumnName = row["FK_COLUMN_NAME"].ToString(); // Имя внешнего столбца
                string pkTableName = row["PK_TABLE_NAME"].ToString(); // Таблица с первичным ключом
                string pkColumnName = row["PK_COLUMN_NAME"].ToString(); // Имя первичного столбца
                string fkName = row["FK_NAME"].ToString(); // Имя внешнего ключа

                relations.Add(new Relation()
                {
                    pkTableName = pkTableName,
                    pkColumnName = pkColumnName,
                    fkTableName = fkTableName,
                    fkColumnName = fkColumnName,
                    fkName = fkName,
                });
            }

            return relations;
        }
        public Dictionary<string, int> GetSkisStats()
        {
            string query = "SELECT\n" +
                "[Лыжи].[Модель] AS SkiName,\n" +
                "COUNT([Аренда].[ID_Лыж]) AS RentCount\n" +
                "FROM [Лыжи] INNER JOIN [Аренда]\n" +
                "ON [Лыжи].[ID] = [Аренда].[ID_Лыж]\n" +
                "GROUP BY [Лыжи].[Модель]\n" +
                "ORDER BY COUNT([Аренда].[ID_Лыж]) DESC;";

            OleDbCommand command = new OleDbCommand(query, mc);

            Dictionary<string, int> stats = new Dictionary<string, int>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    string model = r.GetString(r.GetOrdinal("SkiName"));
                    int count = r.GetInt32(r.GetOrdinal("RentCount"));
                    stats[model] = count;
                }
            }

            return stats;
        }
    }
}
