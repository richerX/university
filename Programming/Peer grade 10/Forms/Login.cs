using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients
{
    public partial class Login : Form
    {
        public Form parentForm;
        public bool isMerchant;

        public string adminLogin = "admin@mail.ru";
        public string adminPassword = "password";

        public Login(Form parentForm, bool isMerchant = false)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.isMerchant = isMerchant;
            if (isMerchant)
            {
                MainLabel.Text = "Администратор";
                SecondaryButton.Visible = false;
                LoginBox.Text = adminLogin;
                PasswordBox.Text = adminPassword;
            }
        }

        #region Вход и регистрация

        /// <summary>
        /// Маленькая кнопка войти/регистрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondaryButton_Click(object sender, EventArgs e)
        {
            if (SecondaryButton.Text == "Регистрация")
            {
                MainButton.Text = "Регистрация";
                SecondaryButton.Text = "Войти";
                SetVisability(true);
            }
            else
            {
                MainButton.Text = "Войти";
                SecondaryButton.Text = "Регистрация";
                SetVisability(false);
            }
        }

        /// <summary>
        /// Большая кнопка войти/регистрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainButton_Click(object sender, EventArgs e)
        {
            if (MainButton.Text == "Войти")
            {
                if (isMerchant)
                    AdminLogin();
                else
                    UserLogin();
            }
            else
            {
                Registration();
            }
        }

        /// <summary>
        /// Вход администратора
        /// </summary>
        private void AdminLogin()
        {
            if (LoginBox.Text == adminLogin && PasswordBox.Text == adminPassword)
            {
                new Merchant(this).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        private void UserLogin()
        {
            bool loggedIn = false;
            foreach (var client in Welcome.shop.clients)
            {
                if (LoginBox.Text == client.login && PasswordBox.Text == client.password)
                {
                    loggedIn = true;
                    new ClientWindow(this, client).Show();
                    this.Hide();
                }
            }
            if (!loggedIn)
                MessageBox.Show("Неверный логин или пароль");
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        private void Registration()
        {
            if (!IsValidEmail(LoginBox.Text))
            {
                MessageBox.Show("Логин должен быть корректным email адресом");
                return;
            }
            foreach (var client in Welcome.shop.clients)
            {
                if (client.login.ToLower() == LoginBox.Text.ToLower())
                {
                    MessageBox.Show("Пользователь с таким email адресом уже зарегистрирован");
                    return;
                }
            }

            if (PasswordBox.Text == "" || NameBox.Text == "" || TelephoneBox.Text == "" || AddressBox.Text == "")
            {
                MessageBox.Show("Поля анкеты не могут быть пустыми");
                return;
            }

            Welcome.shop.clients.Add(new Client(NameBox.Text, TelephoneBox.Text, AddressBox.Text, LoginBox.Text, PasswordBox.Text));
            MessageBox.Show("Регистрация прошла успешно");
            NameBox.Text = "";
            TelephoneBox.Text = "";
            AddressBox.Text = "";
            UserLogin();
        }

        #endregion

        #region email

        /// <summary>
        /// Проверяет доменную часть электронной почты и нормализует ее
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string DomainMapper(Match match)
        {
            var idn = new IdnMapping();
            string domainName = idn.GetAscii(match.Groups[2].Value);
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        /// Проверка валидности email адреса
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        #endregion

        #region Дополнительные функции

        /// <summary>
        /// Кнопка назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

        /// <summary>
        /// Скрытие текстовых полей
        /// </summary>
        /// <param name="value"></param>
        private void SetVisability(bool value)
        {
            NameLabel.Visible = value;
            NameBox.Visible = value;
            TelephoneLabel.Visible = value;
            TelephoneBox.Visible = value;
            AddressLabel.Visible = value;
            AddressBox.Visible = value;
        }

        #endregion
    }
}
