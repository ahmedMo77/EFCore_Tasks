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

    // Main class to solve "question 4"
    internal class program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                int courseId = 1;
                
                var students = context.StudentCourses
                    .Where(sc => sc.CourseId == courseId)
                    .OrderBy(sc => sc.Student.EnrollmentDate)
                    .Select(sc => sc.Student)
                    .ToList();
            }
        }
    }
    
    // Student class
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime EnrollmentDate { get; set; }
    
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }

    // course class
    public class Course
    {
      [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }

    // student course class to link class 
    public class StudentCourse
    {
        public int Id { get; set; }
    
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    
    }

    // DbContext
    class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.SqlConStr);
        }
        public DbSet<Student> Ctudents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    
    }
}


