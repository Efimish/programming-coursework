using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;

namespace ViewConsole
{
    public class Program
    {
        static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                "Введите команду:\n" +
                " - 1) Вывести всех клиентов\n" +
                " - 2) Вывести все лыжи\n" +
                " - 3) Вывести всех работников\n" +
                " - 4) Вывести все аренды\n" +
                " - 5) Захешировать пароль\n" +
                " - 6) Проверить пароль\n" +
                " - 7) Вывести все таблицы\n" +
                " - 8) Вывести все связи\n" +
                " - 9) Тест\n" +
                " - Escape) Выход"
            );
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            IRepository<Client> clientRepository = new ClientRepository();
            IRepository<Skis> skisRepository = new SkisRepository();
            IRepository<Employee> employeeRepository = new EmployeeRepository();
            IRepository<Rent> rentRepository = new RentRepository();

            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                PrintHelp();
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.D1:
                        {
                            IEnumerable<Client> clients = clientRepository.GetAll();
                            if (clients.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            foreach(Client client in clients)
                            {
                                Console.WriteLine(
                                    "{" +
                                    "\n  ID = " + client.ID +
                                    "\n  Login = " + client.Login +
                                    "\n  PasswordHash = " + client.PasswordHash +
                                    "\n  FIO = " + client.FIO +
                                    "\n  Phone = " + client.Phone +
                                    "\n  Email = " + client.Email +
                                    "\n  RegistrationDate = " + client.RegistrationDate +
                                    "\n  BonusPoints = " + client.BonusPoints +
                                    "\n}"
                                );
                            }
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            IEnumerable<Skis> skis = skisRepository.GetAll();
                            if (skis.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            foreach (Skis ski in skis)
                            {
                                Console.WriteLine(
                                    "{" +
                                    "\n  ID = " + ski.ID +
                                    "\n  Model = " + ski.Model +
                                    "\n  Size = " + ski.Size +
                                    "\n  Condition = " + ski.Condition +
                                    "\n  PricePerHour = " + ski.PricePerHour +
                                    "\n  Status = " + ski.Status +
                                    "\n}"
                                );
                            }
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            IEnumerable<Employee> employees = employeeRepository.GetAll();
                            if (employees.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            foreach (Employee employee in employees)
                            {
                                Console.WriteLine(
                                    "{" +
                                    "\n  ID = " + employee.ID +
                                    "\n  Login = " + employee.Login +
                                    "\n  PasswordHash = " + employee.PasswordHash +
                                    "\n  FIO = " + employee.FIO +
                                    "\n  Phone = " + employee.Phone +
                                    "\n  Email = " + employee.Email +
                                    "\n  RegistrationDate = " + employee.RegistrationDate +
                                    "\n  Job = " + employee.Job +
                                    "\n}"
                                );
                            }
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            IEnumerable<Rent> rents = rentRepository.GetAll();
                            if (rents.Count() < 1)
                            {
                                Console.WriteLine("Здесь пусто :(");
                                break;
                            }
                            foreach (Rent rent in rents)
                            {
                                Console.WriteLine(
                                    "{" +
                                    "\n  ID = " + rent.ID +
                                    "\n  ClientID = " + rent.ClientID +
                                    "\n  SkisID = " + rent.SkisID +
                                    "\n  StartTime = " + rent.StartTime +
                                    "\n  Price = " + rent.Price +
                                    "\n  Done = " + rent.Done +
                                    "\n}"
                                );
                            }
                            break;
                        }
                    case ConsoleKey.D5:
                        {
                            string password = Console.ReadLine();
                            string hash = SecurePasswordHasher.Hash(password);
                            Console.WriteLine(hash);
                            break;
                        }
                    case ConsoleKey.D6:
                        {
                            string password = Console.ReadLine();
                            string hash = Console.ReadLine();
                            bool good = SecurePasswordHasher.Verify(password, hash);
                            Console.WriteLine(good ? "Верно" : "Не верно");
                            break;
                        }
                    case ConsoleKey.D7:
                        {
                            List<string> tables = databaseManager.GetAllTables().ToList();
                            foreach (string table in tables)
                            {
                                Console.WriteLine(table);
                            }
                            break;
                        }
                    case ConsoleKey.D8:
                        {
                            List<string> connections = databaseManager.GetAllConnections().ToList();
                            foreach (string connection in connections)
                            {
                                Console.WriteLine(connection);
                            }
                            break;
                        }
                    case ConsoleKey.D9:
                        {
                            Client c1 = new Client(0)
                            {
                                Login = "asd",
                                PasswordHash = "$$$",
                                FIO = "III",
                                Phone = "+7",
                                Email = "@@@",
                                RegistrationDate = DateTime.Now,
                                BonusPoints = 0
                            };
                            Client c2 = new Client(0)
                            {
                                Login = "asd",
                                PasswordHash = "$$$",
                                FIO = "III",
                                Phone = "+7",
                                Email = "@@@",
                                RegistrationDate = DateTime.Now,
                                BonusPoints = 0
                            };
                            Console.WriteLine(c1.Equals(c2));

                            List<int> nums = new List<int>() { 1, 2, 3, 4, 5 };
                            int? n1 = nums.Where(n => n > 10).FirstOrDefault();
                            Console.WriteLine(n1);

                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
    }
}