using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IRepository<T>
        where T : class, IDomainObject
    {
        void Add(T item);
        T Get(int id);
        IEnumerable<T> GetAll(string filter = null);
        void Update(T item);
        void Delete(int id);
    }
}
