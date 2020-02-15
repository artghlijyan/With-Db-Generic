using DbFramework.Attributes;

namespace DbConsoleApp.GlobalObjects.Models
{
    [TableName("Student")]
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
