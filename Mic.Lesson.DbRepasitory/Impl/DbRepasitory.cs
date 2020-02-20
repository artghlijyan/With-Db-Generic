using DbFramework.Attributes;
using DbFramework.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mic.Lesson.DbRepasitory.Repasitories.Impl
{
    public class DbRepasitory<TModel> : IRepasitory<TModel>
        where TModel : class, new()
    {
        private readonly DbContext _dbContext;
        private readonly string _tableName;

        public DbRepasitory(string connectionString)
        {
            _dbContext = new DbContext(connectionString);
            _tableName = Mapper.GetTableName<TModel>();
        }

        public IEnumerable<TModel> SelectAll()
        {
            string query = QueryBuilder.BuildSelectQuery(_tableName);
            return _dbContext
                      .AsReadable(query)
                      .Select(r => r.ToModel<TModel>());
        }

        public void Add(TModel model)
        {
            var properties = Mapper.GetPropertiesAndValues(model);
            string query = QueryBuilder.BuildInsertQuery(_tableName, properties.Keys);
            _dbContext.Insert(query, Mapper.MapToSqlParameter(properties));
        }

        public int Update(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = Mapper.GetPropertiesAndValues(model, true);
            string query = QueryBuilder.BuildUpdateQuery(_tableName, propertiesAndValues.Keys);
            return _dbContext.Update(query, Mapper.MapToSqlParameter(propertiesAndValues));
        }

        public bool Delete(int id)
        {
            return _dbContext.Delete(
                QueryBuilder.Delete(_tableName, id));
        }
    }
}
