using PZ3.Models;
using PZ3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ3.Pages
{

    public partial class Autho : Page
    {
        int click;
        public Autho()
        {
            InitializeComponent();
            click = 0;
        }

        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client());
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;
            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Password.Trim();
            Entities2 db = Entities2.GetContext();

            var user = db.User.Where(x => x.Email == login && x.Password == password).FirstOrDefault();
            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.Name.ToString());
                    LoadPage(user.Role.Name.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha();
                    PasswordBox pswbPassword = new PasswordBox();
                    pswbPassword.Clear();
                    //* Вывод капчи

                }
            }
            else if(click > 1)
            {
                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.Name.ToString());
                    LoadPage(user.Role.Name.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                }
            }
        }

        private void LoadPage(string role, User user)
        {
            click = 0;
            switch (role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client());
                    break;
            }
        }
    }

}
