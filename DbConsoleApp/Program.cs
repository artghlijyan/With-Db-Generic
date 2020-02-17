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
                Id = 4,
                Name = "YSEU",
                PhoneNumber = "+37410444444",
                Address = "Alek Manukyan str.",
            };

            //dbUniRepo.ExecuteInsert(university);
            //System.Console.WriteLine(dbUniRepo.ExecuteDelete("University", 4));

            var uniList = dbUniRepo.ExecuteSelect(new University());
            List<University> unilt = uniList.ToList();

            foreach (var item in uniList)
            {
                //unilt.Add(item);////////////////////////// xi listi mech menak verjin objectna mnum?????????????
                System.Console.WriteLine(item);

            }

            System.Console.WriteLine();
            foreach (var item in unilt)
            {
                System.Console.WriteLine(item);
            }

            //IEnumerable<int> enumerable = Count();
            //List<int> asList = enumerable.ToList();

            //foreach (var item in asList)
            //{
            //    System.Console.WriteLine(item);
            //}
        }

        //static IEnumerable<int> Count()
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        yield return i;
        //    }
        //}
    }
}
