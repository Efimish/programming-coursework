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
        private OleDbConnection mc;
        public RentRepository()
        {
            mc = new OleDbConnection(DBConfig.ConnectionString);
            mc.Open();
        }
        public void Add(Rent rent)
        {
            string query = "INSERT INTO Аренда" +
                "(ID_Клиента, ID_Лыж, Время_начала, Время_окончания, Стоимость, Завершено)" +
                "VALUES (@client_id, @skis_id, @start_time, @end_time, @price, @done);";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("@client_id", rent.ClientID);
            command.Parameters.AddWithValue("@skis_id", rent.SkisID);
            command.Parameters.AddWithValue("@start_time", rent.StartTime.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@end_time", rent.EndTime.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@price", rent.Price);
            command.Parameters.AddWithValue("@done", rent.Done);

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
                    rent = new Rent(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        ClientID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        SkisID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
                        StartTime = r.GetDateTime(r.GetOrdinal("Время_начала")),
                        EndTime = r.GetDateTime(r.GetOrdinal("Время_окончания")),
                        Price = r.GetInt32(r.GetOrdinal("Стоимость")),
                        Done = r.GetBoolean(r.GetOrdinal("Завершено"))
                    };
                }
            }

            return rent;
        }
        public IEnumerable<Rent> GetAll(string filter = null)
        {
            string queryFilter = string.IsNullOrEmpty(filter) ? "" : $" WHERE {filter}";
            string query = $"SELECT * FROM Аренда{queryFilter};";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Rent> rents = new List<Rent>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Rent rent = new Rent(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        ClientID = r.GetInt32(r.GetOrdinal("ID_Клиента")),
                        SkisID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
                        StartTime = r.GetDateTime(r.GetOrdinal("Время_начала")),
                        EndTime = r.GetDateTime(r.GetOrdinal("Время_окончания")),
                        Price = r.GetInt32(r.GetOrdinal("Стоимость")),
                        Done = r.GetBoolean(r.GetOrdinal("Завершено"))
                    };

                    rents.Add(rent);
                }
            }

            return rents;
        }
        public void Update(Rent rent)
        {
            string query = "UPDATE Аренда SET\n" +
                "ID_Клиента = @client_id,\n" +
                "ID_Лыж = @skis_id,\n" +
                "Время_начала = @start_time,\n" +
                "Время_окончания = @end_time,\n" +
                "Стоимость = @price,\n" +
                "Завершено = @done\n" +
                "WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("@client_id", rent.ClientID);
            command.Parameters.AddWithValue("@skis_id", rent.SkisID);
            command.Parameters.AddWithValue("@start_time", rent.StartTime.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@end_time", rent.EndTime.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@price", rent.Price);
            command.Parameters.AddWithValue("@done", rent.Done);
            command.Parameters.AddWithValue("@id", rent.ID);

            command.ExecuteNonQuery();
        }
        public void UpdateAll(IEnumerable<Rent> rents)
        {
            // возможно работает не правильно

            // Получаем список старых аренд
            List<Rent> oldRents = GetAll().ToList();
            foreach (Rent rent in rents)
            {
                // Есть ли аренда среди старых
                bool exists = oldRents.Select(c => c.ID).Contains(rent.ID);
                // Если нет - добавляем
                if (!exists) Add(rent);
                else
                {
                    // Если есть - изменилась ли она?
                    Rent oldRent = oldRents.Where(c => c.ID == rent.ID).First();
                    // Если да - обновляем
                    if (!oldRent.Equals(rent)) Update(rent);
                    // Удаляем их из списка старых
                    oldRents.Remove(oldRent);
                }
            }
            // Теперь в списке старых те, что нужно удалить
            foreach (Rent oldRent in oldRents)
            {
                Delete(oldRent.ID);
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Аренда WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}
