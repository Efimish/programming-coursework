using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class RentRepository : IRepository<Rent>
    {
        public static string connectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=db.mdb;";
        private OleDbConnection mc;
        public RentRepository()
        {
            mc = new OleDbConnection(connectionString);
            mc.Open();
        }
        public void Add(Rent rent)
        {
            string query = "INSERT INTO Аренда" +
                "(ID_Клиента, ID_Лыж, ID_Сотрудника, Время_начала, Время_окончания, Стоимость)" +
                "VALUES @client_id, @skis_id, @employee_id, @start_time, @end_time, @price;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("client_id", rent.ClientID);
            command.Parameters.AddWithValue("skis_id", rent.SkisID);
            command.Parameters.AddWithValue("employee_id", rent.EmployeeID);
            command.Parameters.AddWithValue("start_time", rent.StartTime);
            command.Parameters.AddWithValue("end_time", rent.EndTime);
            command.Parameters.AddWithValue("price", rent.Price);

            command.ExecuteNonQuery();
        }
        public Rent Get(int id)
        {
            string query = "SELECT * FROM Аренда WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Rent rent = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    rent = new Rent
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID")),
                        ClientID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        SkisID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
                        EmployeeID = r.GetInt32(r.GetOrdinal("ID_Сотрудника")),
                        StartTime = r.GetDateTime(r.GetOrdinal("Время_начала")),
                        EndTime = r.GetDateTime(r.GetOrdinal("Время_окончания")),
                        Price = r.GetInt32(r.GetOrdinal("Стоимость"))
                    };
                }
            }

            return rent;
        }
        public IEnumerable<Rent> GetAll()
        {
            string query = "SELECT * FROM Аренда;";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Rent> rents = new List<Rent>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Rent rent = new Rent
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID")),
                        ClientID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        SkisID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
                        EmployeeID = r.GetInt32(r.GetOrdinal("ID_Сотрудника")),
                        StartTime = r.GetDateTime(r.GetOrdinal("Время_начала")),
                        EndTime = r.GetDateTime(r.GetOrdinal("Время_окончания")),
                        Price = 0
                    };

                    rents.Add(rent);
                }
            }

            return rents;
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Аренда WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
