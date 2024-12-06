using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logic
{
    public class ClientRepository : IRepository<Client>
    {
        private OleDbConnection mc;
        public ClientRepository()
        {
            mc = new OleDbConnection(DBConfig.ConnectionString);
            mc.Open();
        }
        public void Add(Client client)
        {
            string query = "INSERT INTO Пользователи" +
                "(Логин, Хэш_пароля, ФИО, Телефон, Email, Тип_пользователя, Дата_регистрации, Бонусные_баллы)" +
                "VALUES (@login, @password_hash, @fio, @phone, @email, @type, @date, @points);";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("@login", client.Login);
            command.Parameters.AddWithValue("@password_hash", client.PasswordHash);
            command.Parameters.AddWithValue("@fio", client.FIO);
            command.Parameters.AddWithValue("@phone", client.Phone);
            command.Parameters.AddWithValue("@email", client.Email);
            command.Parameters.AddWithValue("@type", "клиент");
            command.Parameters.AddWithValue("@date", client.RegistrationDate.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@points", client.BonusPoints);

            command.ExecuteNonQuery();
        }
        public Client Get(int id)
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'клиент' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Client client = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    client = new Client(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы")),
                    };
                }
            }

            return client;
        }
        public Client GetByLogin(string login)
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'клиент' AND Логин = @login;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("login", login);

            Client client = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    client = new Client(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы")),
                    };
                }
            }

            return client;
        }
        public IEnumerable<Client> GetAll(string orderBy = "ID")
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'клиент';";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Client> clients = new List<Client>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Client client = new Client(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        BonusPoints = r.GetInt32(r.GetOrdinal("Бонусные_баллы"))
                    };

                    clients.Add(client);
                }
            }

            return clients;
        }
        public void Update(Client client)
        {
            string query = "UPDATE Пользователи SET\n" +
                "Логин = @login,\n" +
                "Хэш_пароля = @password_hash,\n" +
                "ФИО = @fio,\n" +
                "Телефон = @phone,\n" +
                "Email = @email,\n" +
                "Дата_регистрации = @date,\n" +
                "Бонусные_баллы = @points\n" +
                "WHERE Тип_пользователя = 'клиент' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("@login", client.Login);
            command.Parameters.AddWithValue("@password_hash", client.PasswordHash);
            command.Parameters.AddWithValue("@fio", client.FIO);
            command.Parameters.AddWithValue("@phone", client.Phone);
            command.Parameters.AddWithValue("@email", client.Email);
            command.Parameters.AddWithValue("@date", client.RegistrationDate.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@points", client.BonusPoints);
            command.Parameters.AddWithValue("@id", client.ID);

            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Пользователи WHERE Тип_пользователя = 'клиент' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
