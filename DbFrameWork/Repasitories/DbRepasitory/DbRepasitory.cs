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
        private DbContext dbContext;
        private Mapper mapper;

        public DbRepasitory(string connectionString)
        {
            dbContext = new DbContext(connectionString);
            mapper = new Mapper();
        }

        public IEnumerable<TModel> ExecuteSelect(TModel model)
        {
            IEnumerable<IDataReader> reader =
                dbContext.ExecuteSelect(Query.SelectBuilder(mapper.GetTableName(model)));
            
            foreach (var data in reader)
            {
                yield return mapper.InitializeModel(model, data);
            }
        }

        public int ExecuteInsert(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = mapper.GetPropertiesAndValue(model);
            return dbContext.ExecuteInsert
                (Query.InsertBuilder(mapper.GetTableName(model), propertiesAndValues.Keys),
                mapper.MapToSql(propertiesAndValues));
        }

        public int ExecuteUpdate(TModel model)
        {
            IDictionary<string, object> propertiesAndValues = mapper.GetPropertiesAndValue(model);
            Query.UpdateBuilder("ttt", propertiesAndValues);
            return 0;
        }

        private class Mapper
        {
            public SqlParameter[] MapToSql(IDictionary<string, object> parameters)
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

            public string GetTableName(object model)
            {
                Type type = model.GetType();
                TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
                return attribute == null ? type.Name : attribute.TableName;
            }

            public TModel InitializeModel(TModel model, IDataReader reader)
            {
                Type type = typeof(TModel);
                PropertyInfo[] propInfo = type.GetProperties();
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

            public IDictionary<string, object> GetPropertiesAndValue(TModel model)
            {
                Dictionary<string, object> propertiesAndValues = new Dictionary<string, object>();

                Type type = typeof(TModel);
                PropertyInfo[] propInfo = type.GetProperties();

                int i = -1;

                foreach (var prop in propInfo)
                {
                    i++;
                    if (!Attribute.IsDefined(prop, typeof(KeyAttribute)) && prop.GetValue(model) != null)
                    {
                        propertiesAndValues.Add(prop.Name, prop.GetValue(model));
                    }
                }

                return propertiesAndValues;
            }

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
}
