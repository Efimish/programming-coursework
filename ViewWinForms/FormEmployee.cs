﻿using System;
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
        DatabaseManager databaseManager = new DatabaseManager();
        ClientRepository clientRepository = new ClientRepository();
        SkisRepository skisRepository = new SkisRepository();
        RentRepository rentRepository = new RentRepository();
        Employee employee;

        List<Client> clients;
        List<Skis> skis;
        List<Rent> rents;

        public FormEmployee(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;

            ResetLists();

            comboBoxTables.Items.Clear();
            comboBoxTables.Items.Add("Client");
            comboBoxTables.Items.Add("Skis");
            comboBoxTables.Items.Add("Rent");
            comboBoxTables.SelectedIndex = 0;
        }

        public void ResetLists()
        {
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
            if (selected == "Client")
            {
                dataGridViewTable.DataSource = clients;
            }
            if (selected == "Skis")
            {
                dataGridViewTable.DataSource = skis;
            }
            if (selected == "Rent")
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
            if (selected == "Client")
            {
                clients.Add(new Client(0));
            }
            if (selected == "Skis")
            {
                skis.Add(new Skis(0));
            }
            if (selected == "Rent")
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

            if (selected == "Client")
            {
                clients.RemoveAt(selectedIndex);
            }
            if (selected == "Skis")
            {
                skis.RemoveAt(selectedIndex);
            }
            if (selected == "Rent")
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
                if (selected == "Client")
                {
                    clientRepository.UpdateAll(clients);
                }
                if (selected == "Skis")
                {
                    skisRepository.UpdateAll(skis);
                }
                if (selected == "Rent")
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
            FormCreateTable formCreateTable = new FormCreateTable();
            formCreateTable.ShowDialog();
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            try
            {
                databaseManager.BackupDatabase();
                MessageBox.Show("Успех", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSendEmails_Click(object sender, EventArgs e)
        {
            try
            {
                emailManager.SendEmail(
                    clientRepository.GetAll().Select(c => c.Email),
                    "рассылка спама",
                    "всем привет ловите нашу рассылку спама"
                );
                MessageBox.Show("Успех", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
