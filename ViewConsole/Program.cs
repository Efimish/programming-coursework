using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Logic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                " - 7) Тест\n" +
                " - Escape) Выход"
            );
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
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
                            string query = "INSERT INTO Аренда" +
                                            "(ID_Клиента, ID_Лыж, Время_начала, Время_окончания, Стоимость, Завершено)" +
                                            "VALUES (@client_id, @skis_id, @start_time, @end_time, @price, @done);";

                            Rent rent = new Rent
                            {
                                ClientID = 1,
                                SkisID = 2,
                                StartTime = DateTime.Now,
                                EndTime = DateTime.Now,
                                Price = 0,
                                Done = false,
                            };

                            query = query.Replace("@client_id", rent.ClientID.ToString());
                            query = query.Replace("@skis_id", rent.SkisID.ToString());
                            query = query.Replace("@start_time", rent.StartTime.ToString());
                            query = query.Replace("@end_time", rent.EndTime.ToString());
                            query = query.Replace("@price", rent.Price.ToString());
                            query = query.Replace("@done", rent.Done.ToString());

                            Console.WriteLine(query);
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