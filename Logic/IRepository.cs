using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IRepository<T>
        where T : class, IDomainObject, new()
    {
        void Add(T item);
        T GetByID(int id);
        IEnumerable<T> GetAll();
        void Delete(T item);
    }
}
