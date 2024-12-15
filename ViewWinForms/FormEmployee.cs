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
        public SkisRepository skisRepository = new SkisRepository();
        public RentRepository rentRepository = new RentRepository();
        Employee employee;

        List<string> tables;
        List<Client> clients;
        List<Skis> skis;
        List<Rent> rents;

        public FormEmployee(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;

            ResetLists();

            comboBoxTables.Items.Clear();
            foreach (string table in tables)
            {
                comboBoxTables.Items.Add(table);
            }
            comboBoxTables.SelectedIndex = 0;
        }

        public void FillTablesList()
        {
            ResetLists();
            comboBoxTables.Items.Clear();
            foreach (string table in tables)
            {
                comboBoxTables.Items.Add(table);
            }
        }

        public void ResetLists()
        {
            tables = databaseManager.GetAllTables().ToList();
            clients = clientRepository.GetAll().ToList();
            skis = skisRepository.GetAll().ToList();
            rents = rentRepository.GetAll().ToList();
        }

        private void RedrawTable()
        {
            dataGridViewTable.DataSource = null;
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            dataGridViewTable.AllowUserToAddRows = true;
            if (selected == "Пользователи")
            {
                dataGridViewTable.DataSource = clients;
            }
            if (selected == "Лыжи")
            {
                dataGridViewTable.DataSource = skis;
            }
            if (selected == "Аренда")
            {
                dataGridViewTable.DataSource = rents;
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
            if (selected == "Пользователи")
            {
                clients.Add(new Client(0));
            }
            if (selected == "Лыжи")
            {
                skis.Add(new Skis(0));
            }
            if (selected == "Аренда")
            {
                rents.Add(new Rent(0));
            }
            RedrawTable();
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            var cells = dataGridViewTable.SelectedCells;
            if (cells.Count < 1) return;
            int selectedIndex = cells[0].RowIndex;
            if (selectedIndex < 0) return;

            if (selected == "Пользователи")
            {
                clients.RemoveAt(selectedIndex);
            }
            if (selected == "Лыжи")
            {
                skis.RemoveAt(selectedIndex);
            }
            if (selected == "Аренда")
            {
                rents.RemoveAt(selectedIndex);
            }
            RedrawTable();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetLists();
            RedrawTable();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            try
            {
                if (selected == "Пользователи")
                {
                    clientRepository.UpdateAll(clients);
                }
                if (selected == "Лыжи")
                {
                    skisRepository.UpdateAll(skis);
                }
                if (selected == "Аренда")
                {
                    rentRepository.UpdateAll(rents);
                }
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
                MessageBox.Show("Все получилось!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            if (comboBoxTables.SelectedIndex < 0) return;
            string selected = comboBoxTables.SelectedItem as string;

            try
            {
                databaseManager.DeleteTable(selected);
                FillTablesList();
                comboBoxTables.SelectedIndex = 0;
                MessageBox.Show("Все получилось!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception)
            {
                MessageBox.Show("связи сами удаляйте", "Ой", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStats_Click(object sender, EventArgs e)
        {
            FormStats formStats = new FormStats(this);
            formStats.ShowDialog();
        }
    }
}
