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
            IDictionary<string, object> propertiesAndValues = Mapper.GetPropertiesAndValues(model);

            _dbContext.Insert(
                QueryBuilder.Insert(_tableName, propertiesAndValues.Keys),
                Mapper.MapToSqlParameter(propertiesAndValues));
        }

        public int Update(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = Mapper.GetPropertiesAndValues(model, true);

            return _dbContext.Update(
                QueryBuilder.Update(_tableName, propertiesAndValues.Keys),
                Mapper.MapToSqlParameter(propertiesAndValues));
        }

        public bool Delete(int id)
        {
            return _dbContext.Delete(
                QueryBuilder.Delete(_tableName, id));
        }
    }
}
