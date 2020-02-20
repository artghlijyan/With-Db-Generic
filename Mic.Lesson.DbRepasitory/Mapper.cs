using DbFramework.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mic.Lesson.DbRepasitory
{
    static class Mapper
    {
        public static SqlParameter[] MapToSqlParameter(IDictionary<string, object> parameters)
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

        public static string GetTableName<TModel>()
        {
            Type type = typeof(TModel);
            TableNameAttribute attribute = type.GetCustomAttribute<TableNameAttribute>();
            return attribute == null ? type.Name : attribute.TableName;
        }

        public static TModel ToModel<TModel>(this IDataReader reader) 
            where TModel: class, new()
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

        public static IDictionary<string, object> GetPropertiesAndValues<TModel>(TModel model, bool getId = false)
            where TModel : class, new()
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
}