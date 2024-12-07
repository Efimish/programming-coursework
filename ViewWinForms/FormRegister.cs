using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logic;

namespace ViewWinForms
{
    public partial class FormRegister : Form
    {
        ClientRepository clientRepository = new ClientRepository();
        List<Client> clients;

        public FormRegister()
        {
            InitializeComponent();
            clients = clientRepository.GetAll().ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Closed += (s, args) => this.Close();
            this.Hide();
            formLogin.Show();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string FIO = textBoxFIO.Text;
            string phone = textBoxPhone.Text;
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;

            if (
                login.Length < 1 ||
                FIO.Length < 1   ||
                phone.Length < 1 ||
                email.Length < 1 ||
                password.Length < 1
            )
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clients.Where(c => c.Login == login).Count() > 0)
            {
                MessageBox.Show("Этот логин уже занят!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clients.Where(c => c.Phone == phone).Count() > 0)
            {
                MessageBox.Show("Этот номер телефона уже занят!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clients.Where(c => c.Email == email).Count() > 0)
            {
                MessageBox.Show("Этот Email уже занят!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string passwordHash = SecurePasswordHasher.Hash( password );

            Client client = new Client(0)
            {
                Login = login,
                PasswordHash = passwordHash,
                FIO = FIO,
                Phone = phone,
                Email = email,
                RegistrationDate = DateTime.Now,
                BonusPoints = 0
            };

            clientRepository.Add( client );
            client = clientRepository.GetByLogin(login);

            // теперь можем войти

            FormClient formClient = new FormClient(client);
            formClient.Closed += (s, args) => this.Close();
            this.Hide();
            formClient.Show();
        }

        private void buttonHidePassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !textBoxPassword.UseSystemPasswordChar;
        }
    }
}
