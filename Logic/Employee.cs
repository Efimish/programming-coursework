using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Employee : IDomainObject
    {
        public int ID { get; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Job { get; set; }
        public Employee(int id)
        {
            this.ID = id;
        }
        public override string ToString()
        {
            return $"Сотрудник №{ID}: {FIO}";
        }
    }
}
