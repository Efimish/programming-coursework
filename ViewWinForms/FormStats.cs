using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    public partial class FormStats : Form
    {
        FormEmployee formEmployee;

        public FormStats(FormEmployee formEmployee)
        {
            InitializeComponent();
            this.formEmployee = formEmployee;

            chartMostRented.Series.Clear();

            Dictionary<string, int> stats = formEmployee.databaseManager.GetSkisStats();

            string name = "Самые арендуемые лыжи";
            var points = chartMostRented.Series.Add(name).Points;
            foreach (var stat in stats)
            {
                points.AddXY(stat.Key, stat.Value);
            }
        }
    }
}
