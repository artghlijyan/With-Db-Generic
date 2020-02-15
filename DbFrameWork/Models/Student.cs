using DbFramework.Attributes;

namespace DbFramework.Models
{
    [TableName("Student")]
    public class Student
    {
        private int id;

        public int Id { get => this.id; set => id = value; }

        public string FirstName { get; set; }

        public string LastSurname { get; set; }

        public int Age { get; set; }
    }
}
