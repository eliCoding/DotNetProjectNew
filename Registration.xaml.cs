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

        private void BTN_Reset_Click(object sender, RoutedEventArgs e)
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

        private void Window_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_Confirm_Click(object sender, RoutedEventArgs e)
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
                string userName = textBoxEmail.Text;
                string password = passwordBox1.Password;

                if (!ValidatePassword(password))
                {
                    Errormessage.Text = "Password should be 8-15 letters, at least one low case letter, one upper case letter and one digit!";
                    passwordBox1.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    Errormessage.Text = "Confirm password must be same as password.";
                    passwordBoxConfirm.Focus();
                }

                else if (db.ValidUserName(userName))
                {
                    Errormessage.Text = "The UseName has already been regiested!!!";
                    textBoxEmail.Focus();
                }
                else
                {
                    Employee ep = new Employee(0, firstname, lastname, userName, password, 10000.0m);
                    db.AddEmployee(ep);
                    Errormessage.Text = "*******User registered!******";
                }
            }
        }

        static bool ValidatePassword( string password )
        {
            const int MIN_LENGTH =  8 ;
            const int MAX_LENGTH = 15 ;

            if ( password == null ) throw new ArgumentNullException() ;
            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH ;
            bool hasUpperCaseLetter      = false ;
            bool hasLowerCaseLetter      = false ;
            bool hasDecimalDigit         = false ;

            if ( meetsLengthRequirements )
            {
                foreach (char c in password )
                {
                    if      ( char.IsUpper(c) ) hasUpperCaseLetter = true ;
                    else if ( char.IsLower(c) ) hasLowerCaseLetter = true ;
                    else if ( char.IsDigit(c) ) hasDecimalDigit    = true ;
                }
            }

            bool isValid = meetsLengthRequirements
                           && hasUpperCaseLetter
                           && hasLowerCaseLetter
                           && hasDecimalDigit
                ;
            return isValid ;

        }
    }
}
