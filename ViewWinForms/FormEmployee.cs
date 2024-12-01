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
    public partial class FormEmployee : Form
    {
        SkisRepository skisRepository = new SkisRepository();
        Employee employee;

        public FormEmployee(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }
    }
}
