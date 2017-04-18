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
        string[,] items = new string[,]{
                  {"Alexander keith ","Blond","BudWiser","Corona","lager","Staute"},
                  {"Coffee","Green Tea","Tea", "Hot Chocolate","Cafe Latte","Cappuccino"}
               ,{"Lasagna","Pasta","Pizza","Filet Mignon","Steak","Chicken Wing"},
               {"Cheese Burger","General Tso","Koobide","Sushi","Ghormeh Sabzi","Poutine"},
               {"Cheese Cake","Chocolate Cake","Tiramisu","Ice Cream Cake","Ginger Bread","Jelly"},
               {"SYRAH","MERLOT","RIESLING","GEWÜRZTRAMINER","CHARDONNAY","PINOT NOIR"}
        };

        double[,] itemCost = new double[,]{
                  /*{"Alexander keith ","Blond","BudWiser","Corona","lager","Staute"},
                  {"Coffee","Green Tea","Tea", "Hot Chocolate","Cafe Latte","Cappuccino"}
               ,{"Lasagna","Pasta","Pizza","Filet Mignon","Steak","Chicken Wing"},
               {"Cheese Burger","General Tso","Koobide","Sushi","Ghormeh Sabzi","Poutine"},
               {"Cheese Cake","Chocolate Cake","Tiramisu","Ice Cream Cake","Ginger Bread","Jelly"},
               {"SYRAH","MERLOT","RIESLING","GEWÜRZTRAMINER","CHARDONNAY","PINOT NOIR"}*/
        
        };

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
            readProductPrice();



            // LvItems.ItemsSource=db.GetAllCategory();
            //int CategoryId=1;
            //int ProductId=1;


        }

        string currentItemText;
        int currentItemIndex;


        private void readProductPrice()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    ProductPrice[i, j] = db.GetProductbyID(i, j).UnitPrice;
                }
            }
        }
        private void ButtonBeer_Click(object sender, RoutedEventArgs e)
        {
            ItemList(0);
        }
        private void ButtonHotDrink_Click(object sender, RoutedEventArgs e)
        {
            ItemList(1);
        }

        private void ButtonDinner_Click(object sender, RoutedEventArgs e)
        {
            ItemList(2);
        }
        private void ButtonLunch_Click(object sender, RoutedEventArgs e)
        {
            ItemList(3);
        }

        private void ButtonDessert_Click(object sender, RoutedEventArgs e)
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
                int j = i + 1 + (categoryId * 6);
                Shopping p = db.GetProductbyID(categoryId + 1, j);
                itemList.Add(p.ProductName + "  " + p.UnitPrice);
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

            currentItemText = LvItems.SelectedValue.ToString();
            currentItemIndex = LvItems.SelectedIndex;

            Shopping s = new Shopping(currentItemText, 1, 3, 4, 3, 1);
            LvShopping.Items.Add(s);

            int quantity = 0;
            switch (currentItemText)
            {
                case "Coffee":
                    quantity++;
                    break;
                case "Green Tea":
                    break;
                case "Tea":
                    break;
                case "Hot Chocolate":
                    break;
                case "Cafe Latte":
                    break;
                case "Cappuccino":
                    break;


            }


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
