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

namespace PointOfSaleManagementSys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        public int IdOfCategory;
        public decimal[,] ProductPrice = { { 0.00m, 0.00m, 0.00m, 0.00m, 0.00m, 0.00m }, { 0.00m, 0.00m, 0.00m, 0.00m, 0.00m, 0.00m }, { 0.00m, 0.00m, 0.00m, 0.00m, 0.00m, 0.00m }, 
                                         {0.00m, 0.00m, 0.00m, 0.00m,0.00m, 0.00m},{0.00m, 0.00m, 0.00m, 0.00m,0.00m, 0.00m},{0.00m, 0.00m, 0.00m, 0.00m,0.00m, 0.00m},};
        public string[,] ProductName =
                {  {" ","","","","",""},{" ","","","","",""},
                     {" ","","","","",""}, {" ","","","","",""},
                     {" ","","","","",""},  {" ","","","","",""}};
        public int[,] Counts =
                {  {0,0,0,0,0,0},{0,0,0,0,0,0},
                    {0,0,0,0,0,0},{0,0,0,0,0,0},
                    {0,0,0,0,0,0},{0,0,0,0,0,0},};
        //  {  {"Alexander keith ","Blond","BudWiser","Corona","lager","Staute"},
        //           {"Cheese Cake","Chocolate Cake","Tiramisu","Ice Cream Cake","Ginger Bread","Jelly"},
        //           {"Cheese Burger","General Tso","Koobide","Sushi","Ghormeh Sabzi","Poutine"}, 
        //           {"Coffee","Green Tea","Tea", "Hot Chocolate","Cafe Latte","Cappuccino"}
        //       ,{"Lasagna","Pasta","Pizza","Filet Mignon","Steak","Chicken Wing"},
        //       {"SYRAH","MERLOT","RIESLING","GEWÜRZTRAMINER","CHARDONNAY","PINOT NOIR"}
        //};

        //double[,] itemCost = new double[,]{
        //          /*{"Alexander keith ","Blond","BudWiser","Corona","lager","Staute"},
        //          {"Coffee","Green Tea","Tea", "Hot Chocolate","Cafe Latte","Cappuccino"}
        //       ,{"Lasagna","Pasta","Pizza","Filet Mignon","Steak","Chicken Wing"},
        //       {"Cheese Burger","General Tso","Koobide","Sushi","Ghormeh Sabzi","Poutine"},
        //       {"Cheese Cake","Chocolate Cake","Tiramisu","Ice Cream Cake","Ginger Bread","Jelly"},
        //       {"SYRAH","MERLOT","RIESLING","GEWÜRZTRAMINER","CHARDONNAY","PINOT NOIR"}*/

        //};

        double iTax, iSubTotal, iTotal;
        List<Shopping> shoppingList = new List<Shopping>();



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
            ReadProductPrice();
            RefreshShoppingList();
            // LvItems.ItemsSource=db.GetAllCategory();
            //int CategoryId=1;
            //int ProductId=1;


        }

        //string currentItemText;
        //int currentItemIndex;

        private void ReadProductPrice()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int i1 = i + 1 + (j * 6);
                    Shopping p = db.GetProductbyId(j + 1, i1);
                    ProductPrice[j, i] = p.UnitPrice;
                    ProductName[j, i] = p.ProductName;
                    Counts[j, i] = 0;
                }
            }
        }
        private void RefreshShoppingList()
        {
            LvShopping.Items.Clear();
            List<OrderList> list = db.GetAllOrderList();
            foreach (OrderList l in list)
            {
                int categoryId = (l.ProductId-1) / 6;
                int i = l.ProductId - categoryId * 6-1;
                string name = ProductName[categoryId,i];
                Counts[categoryId, i] = l.Quantity;
                Shopping s = new Shopping(l.ProductId, name, l.Quantity, l.UnitPrice, l.Discount, 3, 1);
                LvShopping.Items.Add(s);
            }
            //LvShopping.ItemsSource = list;
            //LvShopping.Items.Refresh();
        }


        private void ButtonBeer_Click(object sender, RoutedEventArgs e)
        {
            ItemList(0);
        }
        private void ButtonDessert_Click(object sender, RoutedEventArgs e)
        {
            ItemList(1);
        }
        private void ButtonLunch_Click(object sender, RoutedEventArgs e)
        {
            ItemList(2);
        }
        private void ButtonHotDrink_Click(object sender, RoutedEventArgs e)
        {
            ItemList(3);
        }
        private void ButtonDinner_Click(object sender, RoutedEventArgs e)
        {
            ItemList(4);
        }
        private void ButtonWine_Click(object sender, RoutedEventArgs e)
        {
            ItemList(5);
        }

        private void ItemList(int categoryId)
        {
            List<string> itemList = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                itemList.Add(ProductName[categoryId, i]);
            }
            LvItems.ItemsSource = itemList;
            IdOfCategory = categoryId;
        }


        private void ApplyDataBinding()
        {
            List<string> itemList = new List<string>();
            // Bind ArrayList with the ListBox
            LvItems.ItemsSource = itemList;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            List<OrderList> oList = new List<OrderList>();
            int idx = LvItems.SelectedIndex;
            decimal unitprice = ProductPrice[IdOfCategory, idx];
            string name = ProductName[IdOfCategory, idx];
            Counts[IdOfCategory, idx]++;
            int quantity = Counts[IdOfCategory, idx];
            int productId = idx + 1 + 6 * IdOfCategory;
            OrderList o = new OrderList(1, productId, quantity, unitprice, 0.1m);
            if (quantity == 1)
            {
            db.AddOrderList(o);
            RefreshShoppingList();
            return;
            }    
            db.UpdateOrderList(o);
           RefreshShoppingList();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            LvShopping.Items.RemoveAt(LvShopping.Items.IndexOf(LvShopping.SelectedItem));
        }

        private void ButtonChekOut_Click(object sender, RoutedEventArgs e)
        {

        }









    }
}
