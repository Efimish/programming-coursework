using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logic;

namespace ViewWinForms
{
    public partial class FormClient : Form
    {
        SkisRepository skisRepository = new SkisRepository();
        RentRepository rentRepository = new RentRepository();
        Client client;
        string orderBy = "Размер";
        List<Rent> currentRents;
        List<Skis> availableSkis;
        List<Skis> clientSkis;

        public FormClient(Client client)
        {
            InitializeComponent();
            this.client = client;

            comboBoxOrderBy.Items.Clear();
            comboBoxOrderBy.Items.Add("Сбросить");
            comboBoxOrderBy.Items.Add("Размер");
            comboBoxOrderBy.Items.Add("Состояние");
            comboBoxOrderBy.Items.Add("Цена за час");
            comboBoxOrderBy.SelectedIndex = 0;
        }

        private void UpdateData()
        {
            List<Skis> skis = skisRepository.GetAll(orderBy).ToList();

            currentRents = rentRepository.GetAll()
                .Where(rent => !rent.Done)
                .ToList();

            availableSkis = skis
                .Where(s => !currentRents.Select(r => r.SkisID).Contains(s.ID))
                .ToList();

            clientSkis = skis
                .Where(s => currentRents.Where(r => r.ClientID == client.ID).Select(r => r.SkisID).Contains(s.ID))
                .ToList();
        }
        private void RedrawLists()
        {
            UpdateData();
            // fill availabe skis list box
            listBoxSkis.Items.Clear();
            foreach (Skis skis in availableSkis)
            {
                listBoxSkis.Items.Add(skis);
            }
            // fill client's skis list box
            listBoxRentedSkis.Items.Clear();
            foreach (Skis skis in clientSkis)
            {
                listBoxRentedSkis.Items.Add(skis);
            }
        }

        private void buttonRent_Click(object sender, EventArgs e)
        {
            if (listBoxSkis.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите лыжи для аренды!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Skis skis = listBoxSkis.SelectedItem as Skis;

            DialogResult result = MessageBox.Show($"Вы точно хотите арендовать {skis}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            Rent rent = new Rent(0)
            {
                ClientID = client.ID,
                SkisID = skis.ID,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Price = 0,
                Done = false,
            };
            rentRepository.Add(rent);
            RedrawLists();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (listBoxRentedSkis.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите лыжи для возврата!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Skis skis = listBoxRentedSkis.SelectedItem as Skis;

            DialogResult result = MessageBox.Show($"Вы точно хотите вернуть {skis}", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            Rent foundRent = currentRents.Where(r => r.SkisID == skis.ID).First();
            foundRent.Done = true;
            foundRent.EndTime = DateTime.Now;
            foundRent.Price = ((foundRent.EndTime - foundRent.StartTime).Seconds + 3599) / 3600 * skis.PricePerHour;

            rentRepository.Update(foundRent);
            RedrawLists();
        }

        private void comboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrderBy.SelectedIndex < 0) return;

            string item = comboBoxOrderBy.SelectedItem as string;
            switch (item)
            {
                case "Размер":
                    {
                        orderBy = "Размер";
                        break;
                    }
                case "Состояние":
                    {
                        orderBy = "Состояние";
                        break;
                    }
                case "Цена за час":
                    {
                        orderBy = "Цена_за_час";
                        break;
                    }
                default:
                    {
                        orderBy = "ID";
                        break;
                    }
            }
            RedrawLists();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Closed += (s, args) => this.Close();
            this.Hide();
            formLogin.Show();
        }
    }
}
