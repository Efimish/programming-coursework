using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
        public void CreateTable(string name, IEnumerable<TableColumn> columns)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendLine($"CREATE TABLE [{name}] (");
            queryBuilder.AppendLine(string.Join(",\n", columns));
            queryBuilder.AppendLine(");");
            string query = queryBuilder.ToString();
            OleDbCommand command = new OleDbCommand(query, mc);
            command.ExecuteNonQuery();
        }
        public void DeleteTable(string name)
        {
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

        public IEnumerable<string> GetAllTables()
        {
            List<string> tables = new List<string>();

            DataTable schemaTable = mc.GetSchema("Tables");
            foreach (DataRow row in schemaTable.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                string tableType = row["TABLE_TYPE"].ToString();

                // Filter out system tables
                if (tableType == "TABLE")
                {
                    tables.Add(tableName);
                }
            }

            return tables;
        }

        public IEnumerable<string> GetAllConnections()
        {
            // WIP !!!
            List<string> connections = new List<string>();

            //DataTable foreignKeys = mc.GetSchema("ForeignKeys");
            //foreach (DataRow row in foreignKeys.Rows)
            //{
            //    // Get the relevant columns for the foreign key relationship
            //    string fkTableName = row["FK_TABLE_NAME"].ToString(); // The table with the foreign key
            //    string fkColumnName = row["FK_COLUMN_NAME"].ToString(); // The column with the foreign key
            //    string pkTableName = row["PK_TABLE_NAME"].ToString(); // The referenced table
            //    string pkColumnName = row["PK_COLUMN_NAME"].ToString(); // The referenced column

            //    // Create a descriptive string for the foreign key relationship
            //    string connection = $"Foreign Key: {fkTableName}({fkColumnName}) -> {pkTableName}({pkColumnName})";
            //    connections.Add(connection);
            //}


            //v2



            // Retrieve indexes and relationships schema
            // Get indexes and columns metadata
            DataTable indexes = mc.GetSchema("Indexes");
            DataTable columns = mc.GetSchema("Columns");

            // Iterate over indexes to find relationships (Foreign Keys are usually constraints in indexes)
            foreach (DataRow row in indexes.Rows)
            {
                string fkTableName = row["TABLE_NAME"].ToString();   // Table containing the index
                string fkColumnName = row["COLUMN_NAME"].ToString(); // Column part of the index
                string indexName = row["INDEX_NAME"].ToString();   // Index name
                bool isPrimaryKey = row["PRIMARY_KEY"].ToString() == "True"; // Whether this is part of a primary key
                bool isUnique = row["UNIQUE"].ToString() == "True"; // Whether this index is unique

                if (fkTableName.StartsWith("MSys"))
                    continue;

                if (isUnique && !isPrimaryKey)
                    continue;

                // Foreign keys are not directly marked, so we skip if it’s part of a PK
                //if (!isPrimaryKey)
                //{
                //    // This is where relationships would be inferred manually
                //    // Replace this section with custom mapping if relationships are explicitly known
                //    string fkTableName = tableName;
                //    string fkColumnName = columnName;
                //    string pkTableName = "ReferencedTable";   // Placeholder
                //    string pkColumnName = "ReferencedColumn"; // Placeholder

                //    string connection = $"Foreign Key: {fkTableName}({fkColumnName}) -> {pkTableName}({pkColumnName})";
                //    connections.Add(connection);
                //}
                foreach (DataRow columnRow in columns.Rows)
                {
                    string pkTableName = columnRow["TABLE_NAME"].ToString();   // Table with primary key
                    string pkColumnName = columnRow["COLUMN_NAME"].ToString(); // Column with primary key
                    bool isPrimaryColumn = columnRow["COLUMN_KEY"].ToString() == "True"; // Simulated primary key indicator

                    // Skip the same table (foreign key cannot reference itself) and unrelated columns
                    if (fkTableName == pkTableName || !isPrimaryColumn)
                        continue;

                    // Check if the foreign key column matches the primary key column
                    if (fkColumnName == pkColumnName)
                    {
                        // Create the connection string
                        string connection = $"Foreign Key: {fkTableName}({fkColumnName}) -> {pkTableName}({pkColumnName})";
                        connections.Add(connection);
                        break;
                    }
                }
            }

            return connections;
        }
    }
}
