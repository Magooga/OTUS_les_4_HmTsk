using Microsoft.EntityFrameworkCore;

namespace OTUS_les_4_HmTsk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { FirstName = "Bobr", LastName = "Bobrov", Age = 33 };
                User user2 = new User { FirstName = "Hobr", LastName = "Hobrov", Age = 12 };


                // add in db
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Objects saved success! \n");

                var users = db.Users.ToList();
                Console.WriteLine("List of objects:\n");
                foreach (var u in users)
                { 
                    Console.WriteLine($"{u.Id}.{u.FirstName}.{u.LastName} - {u.Age}");
                }
            }

            Console.ReadKey();
        }
    }

    public class User
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class ApplicationContext : DbContext
    { 
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Avito;User Id=postgres;Password=1234;");
            
        }
    }
}