using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        internal static object locker = new object();
        internal static List<Client> clients = new List<Client>();
        static void Main(string[] args)
        {
            try
            {
                Timer timer = new Timer(TimerCheckBalance, null, 0, 5000);
                var loop = true;
                while (loop)
                {
                    Console.Write("Выберите операцию:\n\n1. Создать клиента;\n2. Показать данные клиента;\n3. Удалить клиента;\n4. Обновить баланс клиента;\n5. Выход.\n\nВвод: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Thread insertThread = new Thread(Client.Insert);
                            insertThread.Start();
                            insertThread.Join();
                            Console.WriteLine("\nВведите любую клавишу . . . ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Thread selectThread = new Thread(Client.Select);
                            selectThread.Start();
                            selectThread.Join();
                            Console.WriteLine("\nВведите любую клавишу . . . ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Thread deleteThread = new Thread(Client.Delete);
                            deleteThread.Start();
                            deleteThread.Join();
                            Console.WriteLine("\nВведите любую клавишу . . . ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            Thread updateThread = new Thread(Client.Update);
                            updateThread.Start();
                            updateThread.Join();
                            Console.WriteLine("\nВведите любую клавишу . . . ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Неправильный ввод.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void TimerCheckBalance (object param)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Balance != clients[i].Previous_Balance)
                {
                    if (clients[i].Balance > clients[i].Previous_Balance && clients[i].Transaction_Amount != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n\n\nID: {clients[i].Id},   Прежний баланс: {clients[i].Previous_Balance},   Нынешний баланc: {clients[i].Balance},   Изменение:   {clients[i].Transaction_Type}{clients[i].Transaction_Amount}");
                        Console.ResetColor();
                    }

                    if (clients[i].Balance < clients[i].Previous_Balance && clients[i].Transaction_Amount != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n\n\nID: {clients[i].Id},   Прежний баланс: {clients[i].Previous_Balance},   Нынешний баланc: {clients[i].Balance},   Изменение: {clients[i].Transaction_Type}{clients[i].Transaction_Amount}");
                        Console.ResetColor();
                    }
                }
            }
        }

    }
        
}
