using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using BusinessLogic;
using Logic;

namespace Kursovaya
{
    public partial class FormMain : Form
    {
        public Repository<Client> clientRepository;
        public Repository<Skis> skisRepository;
        public Repository<Employee> employeeRepository;
        public Repository<Rent> rentRepository;
        public FormMain()
        {
            InitializeComponent();
            clientRepository = new Repository<Client>();
            skisRepository = new Repository<Skis>();
            employeeRepository = new Repository<Employee>();
            rentRepository = new Repository<Rent>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
