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

            var rents = formEmployee.rentRepository.GetAll();
            var skis = formEmployee.skisRepository.GetAll();
            var skisDict = skis.ToDictionary(s => s.ID, s => s.Model);
            var rentsDict = new Dictionary<string, int>();
            foreach(var ski in skisDict)
            {
                int amount = rents.Count(r => r.SkisID == ski.Key);
                rentsDict.Add(ski.Value, amount);
            }

            string name = "Самые арендуемые лыжи";
            var points = chartMostRented.Series.Add(name).Points;
            foreach (var rent in rentsDict.OrderByDescending(r => r.Value))
            {
                points.AddXY(rent.Key, rent.Value);
            }
        }
    }
}
