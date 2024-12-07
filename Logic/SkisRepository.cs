using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SkisRepository : IRepository<Skis>
    {
        private OleDbConnection mc;
        public SkisRepository()
        {
            mc = new OleDbConnection(DBConfig.ConnectionString);
            mc.Open();
        }
        public void Add(Skis skis)
        {
            string query = "INSERT INTO Лыжи" +
                "(Модель, Размер, Состояние, Цена_за_час, Статус)" +
                "VALUES @model, @size, @condition, @price, @status;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("model", skis.Model);
            command.Parameters.AddWithValue("size", skis.Size);
            command.Parameters.AddWithValue("condition", skis.Condition);
            command.Parameters.AddWithValue("price", skis.PricePerHour);
            command.Parameters.AddWithValue("status", skis.Status);

            command.ExecuteNonQuery();
        }
        public Skis Get(int id)
        {
            string query = "SELECT * FROM Лыжи WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Skis skis = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    skis = new Skis(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        Model = r.GetString(r.GetOrdinal("Модель")),
                        Size = r.GetInt32(r.GetOrdinal("Размер")),
                        Condition = r.GetString(r.GetOrdinal("Состояние")),
                        PricePerHour = r.GetInt32(r.GetOrdinal("Цена_за_час")),
                        Status = r.GetString(r.GetOrdinal("Статус"))
                    };
                }
            }

            return skis;
        }
        public IEnumerable<Skis> GetAll(string orderBy = "ID")
        {
            if (!new List<string> { "Размер", "Состояние", "Цена_за_час" }.Contains(orderBy)) orderBy = "ID";
            string query = "SELECT * FROM Лыжи\n" +
                $"ORDER BY {orderBy};";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Skis> skisList = new List<Skis>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Skis skis = new Skis(r.GetInt32(r.GetOrdinal("ID")))
                    {
                        Model = r.GetString(r.GetOrdinal("Модель")),
                        Size = r.GetInt32(r.GetOrdinal("Размер")),
                        Condition = r.GetString(r.GetOrdinal("Состояние")),
                        PricePerHour = r.GetInt32(r.GetOrdinal("Цена_за_час")),
                        Status = r.GetString(r.GetOrdinal("Статус"))
                    };

                    skisList.Add(skis);
                }
            }

            return skisList;
        }
        public void Update(Skis skis)
        {
            string query = "UPDATE Лыжи SET\n" +
                "Модель = @model,\n" +
                "Размер = @size,\n" +
                "Состояние = @condition,\n" +
                "Цена_за_час = @price,\n" +
                "Статус = @status,\n" +
                "WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("@model", skis.Model);
            command.Parameters.AddWithValue("@size", skis.Size);
            command.Parameters.AddWithValue("@condition", skis.Condition);
            command.Parameters.AddWithValue("@price", skis.PricePerHour);
            command.Parameters.AddWithValue("@status", skis.Status);
            command.Parameters.AddWithValue("@id", skis.ID);

            command.ExecuteNonQuery();
        }
        public void UpdateAll(IEnumerable<Skis> skisList)
        {
            // возможно работает не правильно

            // Получаем список старых лыж
            List<Skis> oldSkisList = GetAll().ToList();
            foreach (Skis skis in skisList)
            {
                // Есть ли лыжи среди старых
                bool exists = oldSkisList.Select(c => c.ID).Contains(skis.ID);
                // Если нет - добавляем
                if (!exists) Add(skis);
                else
                {
                    // Если есть - изменились ли они?
                    Skis oldSkis = oldSkisList.Where(c => c.ID == skis.ID).First();
                    // Если да - обновляем
                    if (!oldSkis.Equals(skis)) Update(skis);
                    // Удаляем их из списка старых
                    oldSkisList.Remove(oldSkis);
                }
            }
            // Теперь в списке старых те, что нужно удалить
            foreach (Skis oldSkis in oldSkisList)
            {
                Delete(oldSkis.ID);
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Лыжи WHERE ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
