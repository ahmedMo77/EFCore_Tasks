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
  
    // Student class
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    
        public ICollection<StudentCourse> studentCourses { get; set; }
    }

    // course class
    public class Course
    {
      [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<StudentCourse> studentCourses { get; set; }
    }

    // student course class to link class 
    public class StudentCourse
    {
        public int Id { get; set; }
    
        [ForeignKey("student")]
        public int StudentId { get; set; }
        public Student student { get; set; }
    
        [ForeignKey("course")]
        public int CourseId { get; set; }
        public Course course { get; set; }
    
    }

    // DbContext
    class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.SqlConStr);
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
    
    }
}


