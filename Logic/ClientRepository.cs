using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ClientRepository : IRepository<Client>
    {
        public static string connectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=db.mdb;";
        private OleDbConnection mc;
        public ClientRepository()
        {
            mc = new OleDbConnection(connectionString);
            mc.Open();
        }
        public void Add(Client client)
        {
            string query = "INSERT INTP Клиенты" +
                "(ФИО, Номер_телефона, Email, Дата_регистрации, Бонусные_баллы, Хэш_пароля)" +
                "VALUES @fio, @phone, @email, @date, @points, @password_hash;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("fio", client.FIO);
            command.Parameters.AddWithValue("phone", client.Phone);
            command.Parameters.AddWithValue("email", client.Email);
            command.Parameters.AddWithValue("date", client.RegistrationDate);
            command.Parameters.AddWithValue("points", client.BonusPoints);
            command.Parameters.AddWithValue("password_hash", client.PasswordHash);

            command.ExecuteNonQuery();
        }
        public Client Get(int id)
        {
            string query = "SELECT * FROM Клиенты WHERE ID_Клиента = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Client client = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    client = new Client
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Номер_телефона")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля"))
                    };
                }
            }

            return client;
        }
        public Client GetByPhone(string phone)
        {
            string query = "SELECT * FROM Клиенты WHERE Номер_телефона = @phone;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("phone", phone);

            Client client = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    client = new Client
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Номер_телефона")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля"))
                    };
                }
            }

            return client;
        }
        public IEnumerable<Client> GetAll()
        {
            string query = "SELECT * FROM Клиенты;";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Client> clients = new List<Client>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Client client = new Client
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Номер_телефона")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля"))
                    };

                    clients.Add(client);
                }
            }

            return clients;
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Клиенты WHERE ID_Клиента = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
