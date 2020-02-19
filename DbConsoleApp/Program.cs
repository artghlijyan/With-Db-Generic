using DbConsoleApp.GlobalObjects;
using DbConsoleApp.GlobalObjects.Models;
using DbFramework.Repasitories.DbRepasitory;
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
                Name = "YSEU",
                PhoneNumber = "+37410444444",
                Address = "Alek Manukyan str.",
            };

            //dbUniRepo.ExecuteUpdate(university);
            //dbUniRepo.ExecuteInsert(university);
            //System.Console.WriteLine(dbUniRepo.ExecuteDelete(1000));

            var uniList = dbUniRepo.ExecuteSelect().ToList();

            foreach (var item in uniList)
            {
                System.Console.WriteLine(item);

            }
        }
    }
}
