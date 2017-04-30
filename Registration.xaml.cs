using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Database db;
        public Registration()
        {
            InitializeComponent();
            try
            {
                db = new Database();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Error opening database connection: " + ex.Message);
                Environment.Exit(1);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            Login login = new Login();
            login.Show();
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0 || textBoxFirstName.Text.Length == 0 || textBoxLastName.Text.Length == 0)
            {
                Errormessage.Text = "Enter good Name and UserName !!!";
                //textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;

                if (passwordBox1.Password.Length < 6)
                {
                    Errormessage.Text = "Enter password more than 6 letters!";
                    passwordBox1.Focus();
                }
                if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    Errormessage.Text = "Confirm password must be same as password.";
                    passwordBoxConfirm.Focus();
                }

                if (db.ValidUserName(email))
                {
                    Errormessage.Text = "The UseName has already been regiested!!!";
                    textBoxEmail.Focus();
                }
                else
                {
                    Employee ep = new Employee(0, firstname, lastname, email, password, 10000.0m);
                    db.AddEmployee(ep);
                    Errormessage.Text = "*******User registered!******";
                }
            }
        }
    }
}
