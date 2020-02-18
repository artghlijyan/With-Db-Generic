using System.Collections.Generic;

namespace DbFramework.Repasitories
{
    interface IRepasitory<T>
    {
        IEnumerable<T> ExecuteSelect();

        void ExecuteInsert(T t);

        bool ExecuteDelete(int id);
    }
}
