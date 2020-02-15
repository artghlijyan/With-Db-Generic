using DbConsoleApp.GlobalObjects;
using DbConsoleApp.GlobalObjects.Models;
using DbFramework.Repasitories.DbRepasitory;
using System.Collections.Generic;
using System.Linq;

namespace DbConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student
            {
                Id = 1,
                FirstName = "Arman",
                LastName = "Harutyunyan",
                Age = 31,
            };

            DbRepasitory<Student> dbRepo = new DbRepasitory<Student>(ConnectionStrings.SqlConnectionString);
            List<Student> stList = dbRepo.ExecuteSelect(new Student()).ToList();

            foreach (var item in stList)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
