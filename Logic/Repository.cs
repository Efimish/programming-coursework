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
        public DataContext() : base(
            //"Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=db.accdb;"
            //"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:" +
            //"\\Users\\codyg\\source\\repos\\Лаба с Ефимом 3\\DataAccessLayer\\Database1.mdf;Integrated Security=True"
        ) { }
    }
    public class Repository<T> : IRepository<T>
        where T : class, IDomainObject, new()
    {
        DataContext<T> _context;

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
