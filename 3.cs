using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore3
{
    // i will use Data Anotation not "fluent API"
    // to add migration => add-migration migration_name
    // and to update this migration in db => update-migration

    // Main class
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new OrderContext())
            {
                var order = new Order { OrderDate = DateTime.UtcNow };
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
  
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
    }


    // DbContext
    class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.SqlConStr);
        }
        public DbSet<Order> Orders { get; set; }
    
    }
}
