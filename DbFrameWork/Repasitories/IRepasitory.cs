using System.Collections.Generic;

namespace DbFramework.Repasitories
{
    interface IRepasitory<T>
    {
        IEnumerable<T> ExecuteSelect(T t);

        void ExecuteInsert(T t);

        bool ExecuteDelete(string tableName, int modelId);
    }
}
