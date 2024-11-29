using System;
using System.Windows.Forms;
using Logic;

namespace ViewWinForms
{
    public partial class FormRegister : Form
    {
        ClientRepository clientRepository = new ClientRepository();

        public FormRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
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

            string passwordHash = SecurePasswordHasher.Hash( password );

            Client client = new Client
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

            // теперь можем войти

            // Form Client fff = new ...();
            this.Hide();
            // fff.Show();
        }
    }
}
