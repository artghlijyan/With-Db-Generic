using DbFramework.Attributes;

namespace DbFramework.Models
{
    [TableName("University")]
    public class University
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
