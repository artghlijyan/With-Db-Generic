using DbConsoleApp.GlobalObjects;
using DbConsoleApp.GlobalObjects.Models;
using DbFramework.Repasitories.DbRepasitory;
using System.Collections.Generic;
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

            DbRepasitory<University> dbUniRepo = new DbRepasitory<University>(ConnectionStrings.HomeSqlConnectionString);

            University university = new University()
            {
                Name = "Politechnical University",
                PhoneNumber = "+37410111111",
                Address = "Mashtots str.",
            };

            dbUniRepo.ExecuteInsert(university);

            List<University> uniList = dbUniRepo.ExecuteSelect(new University()).ToList();

            foreach (var item in uniList)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
