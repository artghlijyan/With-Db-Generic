using System.Collections.Generic;

namespace DbFramework.Repasitories
{
    interface IRepasitory<T>
    {
        IEnumerable<T> ExecuteSelect(T t);

        int ExecuteInsert(T t);
    }
}
