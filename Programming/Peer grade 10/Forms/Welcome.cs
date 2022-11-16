using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Clients
{
    public partial class Welcome : Form
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public static Shop shop = new Shop();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Welcome()
        {
            InitializeComponent();
            LoadShop("save.xml");
        }

        #region Дополнительные функции

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Welcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientButton_Click(object sender, EventArgs e)
        {
            new Login(this).Show();
            this.Hide();
        }

        /// <summary>
        /// Вход администратора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MerchantButton_Click(object sender, EventArgs e)
        {
            new Login(this, true).Show();
            this.Hide();
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        public static void Exit()
        {
            SaveShop("save.xml");
            Application.Exit();
        }

        /// <summary>
        /// Кнопка выйти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Кнопка рандомной генерации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            new RandomGenerator().Generation();
            SaveShop("save.xml");
        }

        #endregion

        #region Сохранение и загрузка

        /// <summary>
        /// Сохранение состояние приложения
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveShop(string filename)
        {
            try
            {
                var serializer = new DataContractSerializer(typeof(Shop), new DataContractSerializerSettings { PreserveObjectReferences = true });
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                    serializer.WriteObject(stream, shop);
            }
            catch
            {
                MessageBox.Show("При сохранении произошла ошибка");
            }
        }

        /// <summary>
        /// Загрузка состояние приложения
        /// </summary>
        /// <param name="filename"></param>
        public static void LoadShop(string filename)
        {
            try
            {
                var serializer = new DataContractSerializer(typeof(Shop), new DataContractSerializerSettings { PreserveObjectReferences = true });
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                    shop = (Shop)serializer.ReadObject(stream);
            }
            catch
            {
                MessageBox.Show("При загрузке произошла ошибка");
            }
        }

        #endregion
    }
}
