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
using PointOfSaleManagementSys;

namespace ManagementConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Database db1;

        public MainWindow()
        {
           db1 = new Database();
            InitializeComponent();
            ReadEmployee();
        }

        private void ReadEmployee()
        {
            //LvOrders.Items.Clear();
            //List<Order> orderAll = db1.GetAllOrders();
            //if (orderAll != null)
            //{
            //  //  TabControlConsole.SelectedIndex = 1;
            //    foreach (Order o in orderAll)
            //    {
            //       LvOrders.Items.Add(o);
            //    }
            //}

            LvEmployee.Items.Clear();
            List<Employee> list = db1.GetAllEmployees();
            if (list != null)
            {
             //   TabControlConsole.SelectedIndex = 2;
                foreach (Employee ep in list)
                {
                    LvEmployee.Items.Add(ep);
                }
            }

        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
