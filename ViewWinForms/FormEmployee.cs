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
        EmailManager emailManager = new EmailManager();
        public DatabaseManager databaseManager = new DatabaseManager();
        ClientRepository clientRepository = new ClientRepository();
        SkisRepository skisRepository = new SkisRepository();
        RentRepository rentRepository = new RentRepository();

        readonly Employee employee;
        public FormEmployee(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;

            RefillTablesList();
            comboBoxTables.SelectedIndex = 0;
        }

        public void RefillTablesList()
        {
            comboBoxTables.Items.Clear();
            foreach (string table in databaseManager.GetTables().Keys)
            {
                comboBoxTables.Items.Add(table);
            }
        }

        private void RedrawTable()
        {
            dataGridViewTable.DataSource = null;
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            dataGridViewTable.AllowUserToAddRows = true;
            dataGridViewTable.DataSource = databaseManager.GetTable(selected);
            foreach (DataGridViewColumn column in dataGridViewTable.Columns)
            {
                if (column.Name.ToLower() == "id")
                {
                    column.ReadOnly = true; // ID нельзя изменять
                }
            }
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawTable();
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            DataTable table = dataGridViewTable.DataSource as DataTable;
            DataRow newRow = table.NewRow();
            table.Rows.Add(newRow);
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            var cells = dataGridViewTable.SelectedCells;
            if (cells.Count < 1) return;

            DataTable table = dataGridViewTable.DataSource as DataTable;

            int selectedIndex = cells[0].RowIndex;
            if (selectedIndex < 0 || selectedIndex >= table.Rows.Count) return;

            table.Rows.RemoveAt(selectedIndex);

            //if (selected == "Пользователи")
            //{
            //    clients.RemoveAt(selectedIndex);
            //}
            //if (selected == "Лыжи")
            //{
            //    skis.RemoveAt(selectedIndex);
            //}
            //if (selected == "Аренда")
            //{
            //    rents.RemoveAt(selectedIndex);
            //}
            //RedrawTable();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            RedrawTable();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            try
            {
                //if (selected == "Пользователи")
                //{
                //    clientRepository.UpdateAll(clients);
                //}
                //if (selected == "Лыжи")
                //{
                //    skisRepository.UpdateAll(skis);
                //}
                //if (selected == "Аренда")
                //{
                //    rentRepository.UpdateAll(rents);
                //}
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Данные введены неправильно!\n" +
                    "проверьте, что логин, номер телефона и email не повторяются",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
            RedrawTable();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Closed += (s, args) => this.Close();
            this.Hide();
            formLogin.Show();
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            FormCreateTable formCreateTable = new FormCreateTable(this);
            formCreateTable.ShowDialog();
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            try
            {
                databaseManager.BackupDatabase();
                MessageBox.Show("Бэкап базы успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            DialogResult result = MessageBox.Show($"Вы точно хотите удалить таблицу {selected}?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                databaseManager.DeleteTable(selected);
                RefillTablesList();
                comboBoxTables.SelectedIndex = 0;
                MessageBox.Show("Таблица успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStats_Click(object sender, EventArgs e)
        {
            FormStats formStats = new FormStats(this);
            formStats.ShowDialog();
        }
    }
}
