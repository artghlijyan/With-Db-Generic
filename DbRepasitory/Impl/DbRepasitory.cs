using DbFramework.DbHelper;
using System.Collections.Generic;
using System.Linq;

namespace DbRepasitory.Repasitories.Impl
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
            return _dbContext
                      .AsReadable(QueryBuilder.BuildSelectQuery(_tableName))
                      .Select(r => r.ToModel<TModel>());
        }

        public void Add(TModel model)
        {
            IDictionary<string, object> propAndVal = Mapper.GetPropertiesAndValues(model);

            _dbContext.Insert(QueryBuilder
                .BuildInsertQuery(_tableName, propAndVal.Keys)
                , Mapper.MapToSqlParameter(propAndVal));
        }

        public int Update(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = Mapper.GetPropertiesAndValues(model, true);

            return _dbContext.Update(QueryBuilder
                .BuildUpdateQuery(_tableName, propertiesAndValues.Keys)
                , Mapper.MapToSqlParameter(propertiesAndValues));
        }

        public bool Delete(int id)
        {
            return _dbContext.Delete(QueryBuilder.Delete(_tableName, id));
        }
    }
}
