using System;

namespace DbFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; private set; }

        public TableNameAttribute(string modelname)
        {
            this.TableName = modelname;
        }
    }
}
