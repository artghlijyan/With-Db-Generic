using DbFramework.Attributes;
using DbFramework.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DbFramework.Repasitories.DbRepasitory
{
    public class DbRepasitory<TModel> : IRepasitory<TModel>
        where TModel : class, new()
    {
        private readonly Mapper _mapper;
        private readonly DbContext _dbContext;
        private readonly string _tableName;

        public DbRepasitory(string connectionString)
        {
            _mapper = new Mapper();
            _dbContext = new DbContext(connectionString);
            _tableName = _mapper.GetTableName();
        }

        public IEnumerable<TModel> ExecuteSelect()
        {
            IEnumerable<IDataReader> reader =
                _dbContext.ExecuteSelect(Query.SelectBuilder(_tableName));

            foreach (var data in reader)
            {
                yield return _mapper.InitializeModel(data);
            }
        }

        public void ExecuteInsert(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = _mapper.GetPropertiesAndValues(model);

            _dbContext.ExecuteInsert(
                Query.InsertBuilder(_tableName, propertiesAndValues.Keys),
                _mapper.MapToSqlParameter(propertiesAndValues));
        }

        public int ExecuteUpdate(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = _mapper.GetPropertiesAndValues(model, true);
            
            return _dbContext.ExecuteUpdate(
                Query.UpdateBuilder(_tableName, propertiesAndValues.Keys),
                _mapper.MapToSqlParameter(propertiesAndValues));
        }

        public bool ExecuteDelete(int id)
        {
            return _dbContext.ExecuteDelete(
                Query.DeleteBuilder(_tableName, id));
        }

        #region Nested Class Mapper
        private class Mapper
        {
            public SqlParameter[] MapToSqlParameter(IDictionary<string, object> parameters)
            {
                SqlParameter sqlParameter;
                SqlParameter[] sqlParameters = new SqlParameter[parameters.Count];

                for (int i = 0; i < parameters.Count; i++)
                {
                    sqlParameter = new SqlParameter(parameters.Keys.ElementAt(i), parameters.Values.ElementAt(i));
                    sqlParameters[i] = sqlParameter;
                }

                return sqlParameters;
            }

            public string GetTableName()
            {
                Type type = typeof(TModel);
                TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
                return attribute == null ? type.Name : attribute.TableName;
            }

            public TModel InitializeModel(IDataReader reader)
            {
                Type type = typeof(TModel);
                PropertyInfo[] propInfo = type.GetProperties();
                TModel model = new TModel();

                int i = -1;

                foreach (var prop in propInfo)
                {
                    i++;
                    if (!reader.IsDBNull(i))
                        prop.SetValue(model, reader.GetValue(i));
                    else
                        prop.SetValue(model, null);
                }

                return model;
            }

            public IDictionary<string, object> GetPropertiesAndValues(TModel model, bool getId = false)
            {
                IDictionary<string, object> propertiesAndValues = new Dictionary<string, object>();

                Type type = typeof(TModel);
                PropertyInfo[] propInfo = type.GetProperties();

                int i = -1;

                foreach (var prop in propInfo)
                {
                    i++;
                    switch (getId)
                    {
                        case true:
                            {
                                if (prop.GetValue(model) != null)
                                {
                                    propertiesAndValues.Add(prop.Name, prop.GetValue(model));
                                }
                                break;
                            }
                        default:
                            {
                                if (!Attribute.IsDefined(prop, typeof(KeyAttribute)) && prop.GetValue(model) != null)
                                {
                                    propertiesAndValues.Add(prop.Name, prop.GetValue(model));
                                }
                                break;
                            }
                    }
                }

                return propertiesAndValues;
            }
        }


        #endregion

        //public static T ToModel<T>(this IDataReader dataReader) where T : class, new()
        //{
        //    Type type = typeof(T);

        //    var members = type
        //        .GetProperties()
        //        .Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null)
        //        .ToList();

        //    var source = new T();

        //    for (int i = 0; i < members.Count; i++)
        //    {
        //        if (members[i].GetCustomAttribute<DateAttribute>() != null)
        //        {
        //            if (DateTime.TryParse(dataReader.GetValue(i).ToString(), out DateTime date))
        //            {
        //                members[i].SetValue(source, date);
        //            }

        //        }
        //        else
        //        {

        //            if (!dataReader.IsDBNull(i))
        //            {
        //                members[i].SetValue(source, dataReader.GetValue(i));
        //            }
        //            else
        //            {
        //                members[i].SetValue(source, null);
        //            }
        //        }

        //    }

        //    return source;
        //}

    }
}
