using DbFramework.Attributes;
using DbFramework.DbConnector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DbFramework.Repasitories
{
    public class DbRepasitory<TModel> : IRepasitory<TModel>
        where TModel : class, new()
    {
        private DbContext dbContext;

        public DbRepasitory(string connectionString)
        {
            dbContext = new DbContext(connectionString);
        }

        public IEnumerable<TModel> ExecuteSelect()
        {
            
        }

        private class DataToModel
        {
            string GetTableName(object model)
            {
                Type type = model.GetType();
                TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
                return attribute == null ? type.Name : attribute.TableName;
            }

            TModel GetProperties(TModel model)
            {
                Type type = typeof(TModel);
            }
        }
    }
}
