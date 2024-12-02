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
            string query = "INSERT INTO Пользователи" +
                "(Логин, Хэш_пароля, ФИО, Телефон, Email, Тип_пользователя, Дата_регистрации, Должность_сотрудника)" +
                "VALUES @login, @password_hash, @fio, @phone, @email, @type, @date, @job;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("login", employee.Login);
            command.Parameters.AddWithValue("password_hash", employee.PasswordHash);
            command.Parameters.AddWithValue("fio", employee.FIO);
            command.Parameters.AddWithValue("phone", employee.Phone);
            command.Parameters.AddWithValue("email", employee.Email);
            command.Parameters.AddWithValue("type", "сотрудник");
            command.Parameters.AddWithValue("date", employee.RegistrationDate);
            command.Parameters.AddWithValue("job", employee.Job);

            command.ExecuteNonQuery();
        }
        public Employee Get(int id)
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'сотрудник' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            Employee employee = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    employee = new Employee
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID")),
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        Job = r.GetString(r.GetOrdinal("Должность_сотрудника")),
                    };
                }
            }

            return employee;
        }
        public Employee GetByLogin(string login)
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'сотрудник' AND Логин = @login;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("login", login);

            Employee employee = null;

            using (OleDbDataReader r = command.ExecuteReader())
            {
                if (r.Read()) // Check if a row is returned
                {
                    employee = new Employee
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID")),
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        Job = r.GetString(r.GetOrdinal("Должность_сотрудника")),
                    };
                }
            }

            return employee;
        }
        public IEnumerable<Employee> GetAll(string orderBy = "ID")
        {
            string query = "SELECT * FROM Пользователи WHERE Тип_пользователя = 'сотрудник';";
            OleDbCommand command = new OleDbCommand(query, mc);

            List<Employee> employees = new List<Employee>();

            using (OleDbDataReader r = command.ExecuteReader())
            {
                while (r.Read()) // While we get rows
                {
                    Employee employee = new Employee
                    {
                        ID = r.GetInt32(r.GetOrdinal("ID")),
                        Login = r.GetString(r.GetOrdinal("Логин")),
                        PasswordHash = r.GetString(r.GetOrdinal("Хэш_пароля")),
                        FIO = r.GetString(r.GetOrdinal("ФИО")),
                        Phone = r.GetString(r.GetOrdinal("Телефон")),
                        Email = r.GetString(r.GetOrdinal("Email")),
                        RegistrationDate = r.GetDateTime(r.GetOrdinal("Дата_регистрации")),
                        Job = r.GetString(r.GetOrdinal("Должность_сотрудника")),
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }
        public void Update(Employee employee)
        {
            string query = "UPDATE Пользователи SET\n" +
                "Логин = @login,\n" +
                "Хэш_пароля = @password_hash,\n" +
                "ФИО = @fio,\n" +
                "Телефон = @phone,\n" +
                "Email = @email,\n" +
                "Дата_регистрации = @date,\n" +
                "Бонусные_баллы = @points\n" +
                "WHERE Тип_пользователя = 'сотрудник' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);

            command.Parameters.AddWithValue("@login", employee.Login);
            command.Parameters.AddWithValue("@password_hash", employee.PasswordHash);
            command.Parameters.AddWithValue("@fio", employee.FIO);
            command.Parameters.AddWithValue("@phone", employee.Phone);
            command.Parameters.AddWithValue("@email", employee.Email);
            command.Parameters.AddWithValue("@date", employee.RegistrationDate.ToString("dd.MM.yyyy HH:mm"));
            command.Parameters.AddWithValue("@job", employee.Job);
            command.Parameters.AddWithValue("@id", employee.ID);

            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Пользователи WHERE Тип_пользователя = 'сотрудник' AND ID = @id;";
            OleDbCommand command = new OleDbCommand(query, mc);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }
    }
}
