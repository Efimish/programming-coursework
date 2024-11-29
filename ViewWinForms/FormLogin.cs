using Logic;
using System;
using System.Windows.Forms;

namespace ViewWinForms
{
    public partial class FormLogin : Form
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        ClientRepository clientRepository = new ClientRepository();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            if (login.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Employee employee = employeeRepository.GetByLogin(login);
            string type;
            string passwordHash;
            if (employee != null)
            {
                type = "сотрудник";
                passwordHash = employee.PasswordHash;
            } else
            {
                Client client = clientRepository.GetByLogin(login);
                if (client != null)
                {
                    type = "клиент";
                    passwordHash = client.PasswordHash;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxPassword.Text = "";
                    return;
                }
            }

            if (!SecurePasswordHasher.Verify(password, passwordHash))
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Text = "";
                return;
            }

            // вошли в систему

            if (type == "сотрудник") // как сотрудник
            {
                // Form Sotrudnik fff = new ...();
                this.Hide();
                // fff.Show();
            }
            else // как клиент
            {
                // Form Client fff = new ...();
                this.Hide();
                // fff.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            this.Hide();
            formRegister.Show();
        }
    }
}
