using System;
using System.Collections.Generic;
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
        public void BackupDatabase()
        {
            string pathToDatabase = DBConfig.PathToFolder + @"\db.mdb";
            string time = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string pathToDatabaseBackup = DBConfig.PathToFolder + $@"\db-backup_{time}.mdb";
            System.IO.File.Copy(pathToDatabase, pathToDatabaseBackup);
        }
    }
}
