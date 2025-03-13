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
             using (var context = new AppDbContext())
            {
                var product1 = new  { Name = "Labtop", Price = 12000, CategoryId = 1 };
                var product2 = new  { Name = "T-shirt", Price = 800, CategoryId = 2 };
                var product3 = new  { Name = "Camera", Price = 7000, CategoryId = 1 };
            
                var category1 = new Category { Name = "Electronics" };
                var category2 = new Category { Name = "Clothing" };
            }
        }
    }
  
    // Student class
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }

    // course class
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Product> products { get; set; }
    }

    // DbContext
    class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.SqlConStr);
        }
        public DbSet<Category> categorys { get; set; }
        public DbSet<Product> Products { get; set; }
    
    }
}
