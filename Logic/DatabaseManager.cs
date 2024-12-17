using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"CREATE TABLE [{name}] (");
            queryBuilder.AppendLine(string.Join(",\n", columns));
            queryBuilder.AppendLine(");");
            string query = queryBuilder.ToString();
            OleDbCommand command = new OleDbCommand(query, mc);
            command.ExecuteNonQuery();

            if (string.IsNullOrEmpty(connectToTable)) return;

            string primaryKey = columns.Where(c => c.Key).First().Name;
            string newColumn = $"{name}_ID";


            // add a new column
            string addColumnQuery =
                $"ALTER TABLE[{connectToTable}]\n" +
                $"ADD COLUMN[{newColumn}] INTEGER;";
            OleDbCommand addColumnCommand = new OleDbCommand(addColumnQuery, mc);
            addColumnCommand.ExecuteNonQuery();


            // add a new relation
            Relation relation = new Relation()
            {
                FromTable = name,
                FromColumn = primaryKey,
                ToTable = connectToTable,
                ToColumn = newColumn,
                RelationName = $"{name}{connectToTable}"
            };

            string relationQuery =
                $"ALTER TABLE [{relation.ToTable}]\n" +
                $"ADD CONSTRAINT [{relation.RelationName}]\n" +
                $"FOREIGN KEY ([{relation.ToColumn}])\n" +
                $"REFERENCES [{relation.FromTable}] ([{relation.FromColumn}]);";
            OleDbCommand relationCommand = new OleDbCommand(relationQuery, mc);
            relationCommand.ExecuteNonQuery();
        }
        public void DeleteTable(string name)
        {
            List<Relation> relations = GetRelations()
                .Where(r => r.FromTable == name || r.ToTable == name)
                .ToList();
            foreach(Relation relation in relations)
            {
                string relationQuery = $"ALTER TABLE [{relation.ToTable}] DROP CONSTRAINT [{relation.RelationName}]";
                OleDbCommand relationCommand = new OleDbCommand(relationQuery, mc);
                relationCommand.ExecuteNonQuery();
            }
            string query = $"DROP TABLE [{name}];";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.ExecuteNonQuery();
        }
        public void BackupDatabase()
        {
            string pathToDatabase = DBConfig.PathToFolder + @"\db.mdb";
            string time = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string pathToDatabaseBackup = DBConfig.PathToFolder + $@"\db-backup_{time}.mdb";
            System.IO.File.Copy(pathToDatabase, pathToDatabaseBackup);
        }
        public void CreateRelation(string columnFrom, string columnTo)
        {
            // create it somehow
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
            // WIP !!!
            Dictionary<string, List<string>> tableColumns = GetTables();
            Dictionary<string, string> primaryKeys = GetPrimaryKeys();

            List<Relation> relations = new List<Relation>();

            DataTable schemaIndexes = mc.GetSchema("Indexes");
            foreach (DataRow row in schemaIndexes.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();   // Table containing the index
                string columnName = row["COLUMN_NAME"].ToString(); // Column part of the index
                string indexName = row["INDEX_NAME"].ToString();   // Index name
                bool isPrimaryKey = row["PRIMARY_KEY"].ToString() == "True"; // Whether this is part of a primary key
                bool isUnique = row["UNIQUE"].ToString() == "True"; // Whether this index is unique

                if (!tableColumns.ContainsKey(tableName))
                    continue;

                if (isUnique)
                    continue;

                string otherTable = tableColumns.Keys
                    .Where(t => t != tableName)
                    .Where(t => indexName.Contains(t)).First();
                string otherColumn = primaryKeys[otherTable];

                relations.Add(new Relation()
                {
                    FromTable = otherTable,
                    FromColumn = columnName,
                    ToTable = tableName,
                    ToColumn = columnName,
                    RelationName = indexName,
               });
            }

            return relations;
        }
    }
}
