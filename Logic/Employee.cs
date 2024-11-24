using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Employee : IDomainObject
    {
        public int ID { get; set; }
        public string FIO;
        public string Job;
        public DateTime EmploymentDate;
        public string Phone;
        public string PasswordHash;
    }
}
