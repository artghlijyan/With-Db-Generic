using DbFramework.Attributes;
using DbFramework.DbHelper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DbFramework.Repasitories.DbRepasitory
{
    public class DbRepasitory<TModel> : IRepasitory<TModel>
        where TModel : class, new()
    {
        private DbContext dbContext;
        private DataToModel toModel;
        
        public DbRepasitory(string connectionString)
        {
            dbContext = new DbContext(connectionString);
        }

        public IEnumerable<TModel> ExecuteSelect(TModel model)
        {
            toModel = new DataToModel();
            IEnumerable<IDataReader> reader =
                dbContext.ExecuteSelect(Query.SelectByQuery(Query.QueryType.Select, toModel.GetTableName(model)));

            foreach (var data in reader)
            {
                yield return toModel.GetProperties(model, data);
            }
        }

        private class DataToModel
        {
            public string GetTableName(object model)
            {
                Type type = model.GetType();
                TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
                return attribute == null ? type.Name : attribute.TableName;
            }

            public TModel GetProperties(TModel model, IDataReader reader)
            {
                Type type = typeof(TModel);
                PropertyInfo[] propInfo = type.GetProperties();

                foreach (var prop in propInfo)
                {
                    int i = -1;
                    i++;
                    if (!reader.IsDBNull(i))
                        prop.SetValue(model, reader.GetValue(i));
                }

                return model;
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
