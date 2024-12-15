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
            //string query = "SELECT Name FROM MSysObjects WHERE Type = 1 AND Flags = 0;";
            //OleDbCommand command = new OleDbCommand(query, mc);

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
            //using (OleDbDataReader r = command.ExecuteReader())
            //{
            //    while (r.Read()) // While we get rows
            //    {
            //        tables.Add(r.GetString(0));
            //    }
            //}

            return tables;
        }

        public IEnumerable<string> GetAllConnections()
        {
            List<string> connections = new List<string>();
            return connections;
        }
    }
}
