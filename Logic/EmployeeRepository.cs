using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public static string connectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=db.mdb;";
        private OleDbConnection mc;
        public EmployeeRepository()
        {
            mc = new OleDbConnection(connectionString);
            mc.Open();
        }
        public void Add(Employee employee)
        {
            string query = "INSERT INTP Сотрудники" +
                "(ФИО, Должность, Дата_приема_на_работу, Контактный_телефон, Хэш_пароля)" +
                "VALUES @fio, @job, @date, @phone, @password_hash;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("fio", employee.FIO);
            command.Parameters.AddWithValue("job", employee.Job);
            command.Parameters.AddWithValue("date", employee.EmploymentDate);
            command.Parameters.AddWithValue("phone", employee.Phone);
            command.Parameters.AddWithValue("password_hash", employee.PasswordHash);

            command.ExecuteNonQuery();
        }
        public Employee Get(int id)
        {
            string query = "SELECT * FROM Сотрудники WHERE ID_Сотрудника = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Employee employee = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    employee = new Employee
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Сотрудника")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Job = r.GetString(r.GetOrdinal("Должность")),
                        EmploymentDate = r.GetDateTime(r.GetOrdinal("Дата_приема_на_работу")),
                        Phone = r.GetString(r.GetOrdinal("Контактный_телефон")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля"))
                    };
                }
            }

            return employee;
        }
        public IEnumerable<Employee> GetAll()
        {
            string query = "SELECT * FROM Сотрудники;";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Employee> employees = new List<Employee>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Employee employee = new Employee
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID_Сотрудника")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Job = r.GetString(r.GetOrdinal("Должность")),
                        EmploymentDate = r.GetDateTime(r.GetOrdinal("Дата_приема_на_работу")),
                        Phone = r.GetString(r.GetOrdinal("Контактный_телефон")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля"))
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Сотрудники WHERE ID_Сотрудника = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
