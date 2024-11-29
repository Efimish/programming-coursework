using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewWinForms
{
    public partial class FormLogin : Form
    {
        ClientRepository clientRepository = new ClientRepository();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string phone = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            if (phone.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Client client = clientRepository.GetByPhone(phone);

            bool good = SecurePasswordHasher.Verify(password, client.PasswordHash);

            if (!good)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Text = "";
                return;
            }

            // вошли в систему
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            this.Hide();
            formRegister.Show();
        }
    }
}
