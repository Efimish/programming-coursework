using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DataContext<T> : DbContext
        where T : class, IDomainObject, new()
    {
        public DbSet<T> Items { get; set; }
        public DataContext() : base("name=DataContext") { }
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
    public class Repository<T> : IRepository<T>
        where T : class, IDomainObject, new()
    {
        readonly DataContext<T> _context;

        public Repository()
        {
            _context = new DataContext<T>();
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }
        public T GetByID(int id)
        {
            return _context.Items.Where(x => x.ID == id).FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            return new List<T>(_context.Set<T>());
        }
        public void Delete(T item) {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }
    }
}
