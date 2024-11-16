using BusinessLogic;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                "\n" +
                " - 5) Пункт 5\n" +
                " - 6) Пункт 6\n" +
                " - 7) Пункт 7\n" +
                " - 7) Пункт 8\n" +
                " - Escape) Выход"
            );
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            Repository<Client> clientRepository = new Repository<Client>();
            Repository<Skis> skisRepository = new Repository<Skis>();
            Repository<Employee> employeeRepository = new Repository<Employee>();
            Repository<Rent> rentRepository = new Repository<Rent>();

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
                                Console.WriteLine(client);
                            }
                            break;
                        }
                }
            }
        }
    }
}