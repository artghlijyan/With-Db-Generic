using DbFramework.Attributes;

namespace DbConsoleApp.GlobalObjects.Models
{
    [TableName("Student")]
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }
        
        public override string ToString()
        {
            return $"Id = {Id}, Firstname = {FirstName}, Lastname = {LastName}, Age = {Age}";
        }
    }
}
