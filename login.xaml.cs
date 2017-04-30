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
using System.Windows.Shapes;

namespace PointOfSaleManagementSys
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        Registration registration = new Registration();
        MainWindow Main = new MainWindow();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text.Length == 0)
            {
                Errormessage.Text = "Enter valid UserName or password!";
                TextBoxEmail.Focus();
            }
            string email = TextBoxEmail.Text;

            string password = PasswordBox.Password;

            if (Globas.Db.ValidPassword(email, password))
            {
                Main.Show();
                Close();
            }
            else
            {
                Errormessage.Text = "Sorry! Please enter existing username/password.";
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.ShowDialog();
        }


    }
}
