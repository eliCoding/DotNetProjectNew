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
            try
            {
                db1 = new Database();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
            InitializeComponent();
            ReadAll();
            TabControlConsole.SelectedIndex = 2;

        }

        private void ReadAll()
        {
            ReadEmployee();
            ReadOrders();
            ReadStocks();
        }
        private void ReadOrders()
        {
            LvOrders.Items.Clear();
            List<Order> orderAll = db1.GetAllOrders();
            if (orderAll != null)
            {
                //  TabControlConsole.SelectedIndex = 1;
                foreach (Order o in orderAll)
                {
                    LvOrders.Items.Add(o);
                }
            }
        }
        private void ReadEmployee()
        {
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
        private void ReadStocks()
        {
            LvItems.Items.Clear();
            List<InStock> itemStocks = db1.GetAllProducts();
            if (itemStocks != null)
            {
                //   TabControlConsole.SelectedIndex = 2;
                foreach (InStock ep in itemStocks)
                {
                    LvItems.Items.Add(ep);
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (TabControlConsole.SelectedIndex)
            {
                case 2:
                    if (LvEmployee.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Order Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Employee ep = (Employee)LvEmployee.Items[LvEmployee.SelectedIndex];
                    db1.DeleteEmployeeById(ep.Id);
                    ReadEmployee();
                    break;
                case 1:
                    if (LvOrders.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Order Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Order o = (Order)LvOrders.Items[LvOrders.SelectedIndex];
                    db1.DeleteOrderById(o.OrderId);
                    ReadOrders();
                    break;
                case 0:
                    if (LvItems.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Order Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    InStock ins = (InStock)LvOrders.Items[LvOrders.SelectedIndex];
                    db1.DeleteProductById(ins.Id);
                    ReadStocks();
                    break;
            }

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
