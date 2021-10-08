using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Client
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Previous_Balance { get; set; }
        public string Transaction_Type { get; set; }
        public decimal Transaction_Amount { get; set; }

        internal static void Insert ()
        {
            Client client = new Client();
            Console.Write("\nУкажите ID клиента: ");
            var temp_client_id = int.Parse(Console.ReadLine());
            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (Program.clients[i].Id == temp_client_id)
                {
                    Console.WriteLine("\nКлиент с данным ID уже существует. Пожалуйста, укажите другой ID.");
                    return;
                }
            }
            client.Id = temp_client_id;
            Console.Write("\nУкажите баланс клиента: ");
            client.Balance = decimal.Parse(Console.ReadLine());
            Program.clients.Add(client);
            Console.WriteLine("\nКлиент успешно добавлен.\n");
        }
        internal static void Delete ()
        {
            if (Program.clients.Count == 0)
            {
                Console.WriteLine("\nВ базе пока не имеется ни одного клиента.");
                return;
            }
            Console.Write("\nУкажите ID клиента, которого хотите удалить: ");
            int.TryParse(Console.ReadLine(), out int id);
            int ListIndex = GetListIndex(id);
            if (ListIndex < 0)
            {
                Console.WriteLine("\nДанного клиента не сущетвует.");
                return;
            }
            Program.clients.RemoveAt(ListIndex);
            Console.WriteLine("\nКлиент успешно удален.");

        }
        internal static void Select ()
        {
            if (Program.clients.Count == 0)
            {
                Console.WriteLine("\nВ базе пока не имеется ни одного клиента.");
                return;
            }
            Console.Write("\nУкажите ID клиента, которого хотите показать: ");
            int.TryParse(Console.ReadLine(), out int id);
            int ListIndex = GetListIndex(id);
            if (ListIndex < 0)
            {
                Console.WriteLine("\nДанного клиента не сущетвует.");
                return;
            }
            Console.WriteLine($"\nID: {Program.clients[ListIndex].Id},    Баланс: {Program.clients[ListIndex].Balance},   Предыдущий баланс: {Program.clients[ListIndex].Previous_Balance},   Размер транзации: {Program.clients[ListIndex].Transaction_Type}{Program.clients[ListIndex].Transaction_Amount}\n");
        }
        internal static void Update ()
        {
            if (Program.clients.Count == 0)
            {
                Console.WriteLine("\nВ базе пока не имеется ни одного клиента.");
                return;
            }
            Console.Write("\nУкажите ID клиента, баланс которого хотите обновить: ");
            int.TryParse(Console.ReadLine(), out int id);
            int ListIndex = GetListIndex(id);
            if (ListIndex < 0)
            {
                Console.WriteLine("\nДанного клиента не сущетвует.");
                return;
            }
            Console.Write("\nВведите операцию по счету(+ / -): ");
            var temp_transaction_type = Console.ReadLine();
            if (temp_transaction_type != "+" && temp_transaction_type != "-")
            {
                Console.WriteLine("\nНеправильный ввод.");
                return;
            }
            Program.clients[ListIndex].Transaction_Type = temp_transaction_type;
            Console.Write("\nВведите желаемую сумму: ");
            Program.clients[ListIndex].Transaction_Amount = decimal.Parse(Console.ReadLine());
            Program.clients[ListIndex].Previous_Balance = Program.clients[ListIndex].Balance;
            if (Program.clients[ListIndex].Transaction_Type == "+")
                Program.clients[ListIndex].Balance += Program.clients[ListIndex].Transaction_Amount;
            if (Program.clients[ListIndex].Transaction_Type == "-")
                Program.clients[ListIndex].Balance -= Program.clients[ListIndex].Transaction_Amount;
            Console.WriteLine("\nОперация успешно выполнена. ");
           
        }

        internal static int GetListIndex (int id)
        {
            int ListIndex = -1;

            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (Program.clients[i].Id == id)
                {
                    ListIndex = i;
                }
            }
            return ListIndex;
        }
    }
}
