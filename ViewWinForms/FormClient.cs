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
        List<Rent> currentRents;
        List<Skis> availableSkis;
        List<Skis> clientSkis;

        public FormClient(Client client)
        {
            InitializeComponent();
            this.client = client;
            RedrawLists();
        }

        private void UpdateData()
        {
            currentRents = rentRepository.GetAll()
                .Where(rent => !rent.Done)
                .ToList();

            availableSkis = skisRepository.GetAll()
                .Where(skis => !currentRents.Select(r => r.SkisID).Contains(skis.ID))
                .ToList();

            clientSkis = skisRepository.GetAll()
                .Where(skis => currentRents.Where(r => r.ClientID == client.ID).Select(r => r.SkisID).Contains(skis.ID))
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

            Rent rent = new Rent
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
            foundRent.Price = (foundRent.EndTime - foundRent.StartTime).Seconds / 3600 * skis.PricePerHour;

            //rentRepository.Update(foundRent);
            RedrawLists();
        }
    }
}
