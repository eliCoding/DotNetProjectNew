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
        private void RefreshInStockList()
        {
            LvItems.ItemsSource = db.GetAllInStock();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = LvItems.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please select Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            InStock i = (InStock)LvItems.Items[index];
            try
            {
                db.DeleteInStockListById(i.Id);
                RefreshInStockList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Database query error " + ex.Message);
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
            /* List<OrderList> oList = new List<OrderList>();
                        int idx = LvItems.SelectedIndex;
           
                        if (idx < 0)
                        {
                            MessageBox.Show("Please select Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        decimal unitprice = ProductPrice[IdOfCategory, idx];
                        string name = ProductName[IdOfCategory, idx];
                        Counts[IdOfCategory, idx]++;
                        int quantity = Counts[IdOfCategory, idx];
                        int productId = idx + 1 + 6 * IdOfCategory;
                        OrderList o = new OrderList(1, productId, quantity, unitprice, 0.1m);
                        if (quantity ==1)
                        {
                            db.AddOrderList(o);
                            RefreshShoppingList();
                            return;
                        }
                        db.UpdateOrderList(o);
                        RefreshShoppingList();*/
=======

        private Database db1;

        public MainWindow()
        {
<<<<<<< HEAD
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
