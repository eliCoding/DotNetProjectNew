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

namespace PointOfSaleManagementSys.ManagementConsole
{
    //PointOfSaleManagementSys.Database db1;
    ////PointOfSaleManagementSys.PrintInvoice pi;
    ////public int IdOfCategory;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
<<<<<<< HEAD
        Database db;
        public MainWindow()
        {
           
            try
            {
                db = new Database();


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error opening database connection: " + e.Message);
                Environment.Exit(1);
            }
            InitializeComponent();
            RefreshInStockList();
        }
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



        public MainWindow()
        {
            //db1 = new PointOfSaleManagementSys.Database();
            
            //InitializeComponent();
>>>>>>> 6236c628d3702cb1fab4f8ee9f4ab2cdb1a33e79
        }
    }
}
