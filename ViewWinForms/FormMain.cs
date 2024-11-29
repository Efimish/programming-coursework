using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace ViewWinForms
{
    public partial class FormMain : Form
    {
        public IRepository<Client> clientRepository;
        public IRepository<Skis> skisRepository;
        public IRepository<Employee> employeeRepository;
        public IRepository<Rent> rentRepository;
        public FormMain()
        {
            InitializeComponent();
            clientRepository = new ClientRepository();
            skisRepository = new SkisRepository();
            employeeRepository = new EmployeeRepository();
            rentRepository = new RentRepository();
        }
    }
}
