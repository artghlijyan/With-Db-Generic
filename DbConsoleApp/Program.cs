using DbConsoleApp.GlobalObjects;
using Mic.Lesson.DbRepasitory.Models;
using Mic.Lesson.DbRepasitory.Repasitories.Impl;
using System.Linq;

namespace DbConsoleApp
{
    class Program
    {
        static void Main()
        {
            //Student student = new Student
            //{
            //    Id = 1,
            //    FirstName = "Arman",
            //    LastName = "Harutyunyan",
            //    Age = 31,
            //};

            //DbRepasitory<Student> dbStRepo = new DbRepasitory<Student>(ConnectionStrings.SqlConnectionString);
            //List<Student> stList = dbStRepo.ExecuteSelect(new Student()).ToList();

            DbRepasitory<University> dbUniRepo = new DbRepasitory<University>(ConnectionStrings.MicSqlConnectionString);

            University university = new University()
            {
                Name = "YSEU",
                PhoneNumber = "+37410444444",
                Address = "Alek Manukyan str.",
            };

            //dbUniRepo.ExecuteUpdate(university);
            dbUniRepo.Add(university);
            //System.Console.WriteLine(dbUniRepo.ExecuteDelete(1000));

            var uniList = dbUniRepo.SelectAll().ToList();

            foreach (var item in uniList)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
