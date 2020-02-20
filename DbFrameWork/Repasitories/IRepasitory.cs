using System.Collections.Generic;

namespace DbFramework.Repasitories
{
    interface IRepasitory<T>
    {
        IEnumerable<T> SelectAll();

        void Add(T t);

        int Update(T t);

        bool Delete(int id);
    }
}
