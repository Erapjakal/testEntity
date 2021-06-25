using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("настройка подключения к бд...");
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.WriteLine("к базе данных успешно подключено...");
                //Создание обектов для добавления в бд
                Продукт product1 = new Продукт { Name = "Капуста", Price = 5.5, Weight = 18.978 };
                Продукт product2 = new Продукт { Name = "Лук репчатый", Price = 4, Weight = 21.48 };
                //Добавляем созданые ранее объекты в бд
                db.Продукты.Add(product1);
                db.Продукты.Add(product2);
                db.SaveChanges();
                Console.WriteLine("в базу данных внесены изменения...");
                //обращаемся к бд и получаем данные
                var products = db.Продукты.ToList();
                Console.WriteLine("список объектов в базе данных:");
                foreach (var item in db.Продукты)
                {
                    Console.WriteLine($"в базе данных {item.Id}-й элемент");
                    Console.WriteLine($"Название продукта - {item.Name} Цена - { item.Price} Вес {item.Weight}");
                }
            }
        }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Продукт> Продукты { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
    public class Продукт
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
    }
}
