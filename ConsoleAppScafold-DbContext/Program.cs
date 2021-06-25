using System;
using System.Linq;

namespace ConsoleAppScafold_DbContext
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (helloappdbContext db = new helloappdbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Продуктыs.ToList();
                Console.WriteLine("Список объектов:");
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Price} - {u.Weight}");
                }
            }
            Console.ReadKey();
        }
    }
}
