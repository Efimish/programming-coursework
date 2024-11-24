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
        public static string connectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=db.mdb;";
        private OleDbConnection mc;
        public SkisRepository()
        {
            mc = new OleDbConnection(connectionString);
            mc.Open();
        }
        public void Add(Skis skis)
        {
            string query = "INSERT INTP Лыжи" +
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
            string query = "SELECT * FROM Лыжи WHERE ID_Лыж = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Skis skis = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    skis = new Skis
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
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
        public IEnumerable<Skis> GetAll()
        {
            string query = "SELECT * FROM Лыжи;";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Skis> skisList = new List<Skis>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Skis skis = new Skis
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Лыж")),
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
        public void Delete(int id)
        {
            string query = "DELETE FROM Лыжи WHERE ID_Лыж = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
