using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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


        FlowDocument doc = new FlowDocument();



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

        }


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
            decimal total = 0.0m;
            decimal totalTax = 0.0m;
            foreach (OrderList l in list)
            {
                int categoryId = (l.ProductId - 1) / 6;
                int i = l.ProductId - categoryId * 6 - 1;
                string name = ProductName[categoryId, i];
                Counts[categoryId, i] = l.Quantity;
                decimal subtaotal = l.Quantity * l.UnitPrice;
                total = total + subtaotal;
                decimal tax = subtaotal * 0.15m;
                totalTax = totalTax + tax;
                Shopping s = new Shopping(l.ProductId, name, l.Quantity, l.UnitPrice, l.Discount, subtaotal, tax);
                LvShopping.Items.Add(s);
            }

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
            List<ItemList> item = new List<ItemList>();
            LvItems.Items.Clear();
            for (int i = 0; i < 6; i++)
            {
                ItemList it = new ItemList(ProductName[categoryId, i], ProductPrice[categoryId, i]);
                LvItems.Items.Add(it);
            }
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

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            int Id = 1;
            db.DeleteOrderById(Id);
            RefreshShoppingList();

        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = LvShopping.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please select Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Shopping s = (Shopping)LvShopping.Items[index];
            if (s.Quantity > 1)
            {
                int categoryId = (s.ID - 1) / 6;
                int i = s.ID - categoryId * 6 - 1;
                Counts[categoryId, i] = s.Quantity - 1;

                OrderList o = new OrderList(1, s.ID, s.Quantity - 1, s.UnitPrice, 0.1m);
                db.UpdateOrderList(o);
                RefreshShoppingList();
                return;
            }

            try
            {
                db.DeleteOrderListById(s.ID);
                RefreshShoppingList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Database query error " + ex.Message);
            }
        }

        private void ButtonChekOut_Click(object sender, RoutedEventArgs e)
        {
            // Bind Check out Button With Tab COntrol
            int nIndex = TabControl.SelectedIndex + 1;
            if (nIndex < 1)
            {
                nIndex = TabControl.Items.Count + 1;
            }
            TabControl.SelectedIndex = nIndex;

            string theDate = dpDate.Text;
            //.ToString("yyyy-MM-dd");
            string itemPurchasedInfo = "";
            itemPurchasedInfo = "=============================" + "\r\n" + "Mike & Elmira's Company" + "\r\n" + "=============================" + "\r\n" + "" + "Address:" + "\r\n" + "JOhn Abbot College" + "\r\n" + "Phone: 514- 543 74 89" + "\r\n" + "INVOICE NO:  \t\t Date: " + theDate + "\r\n=============================" + "\r\n";
            //string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            for (int i = 0; i < LvShopping.Items.Count; i++)
            {
                Shopping s = (Shopping)LvShopping.Items[i];
                itemPurchasedInfo += "Product Name: " + s.ProductName + "\r\n  Quantity: " + s.Quantity +
                    "\r\n  Unit Price: " + String.Format("{0:C}", s.UnitPrice) + "\r\n\r\n";
            }
            itemPurchasedInfo += "====================" + "\r\n";
            itemPurchasedInfo += "Tax:  " + totalTaxCost.Text + "\r\n";
            itemPurchasedInfo += "Balance:  " + BalancePriceTb.Text + "\r\n";
            itemPurchasedInfo += "Paid:  " + PaidTextBox.Text + "\r\n";
            itemPurchasedInfo += "Method Of Payment:  " + ComboCard.Text;

            itemPurchasedInfo += "\r\n" + "*****************************" + "\r\n" + "Thank you for Shoping at Mike & Elmira's Company";

            try
            {
                string path = @"..\..\Invoice.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(path);
                file.WriteLine(itemPurchasedInfo);

                file.Close();

                // Open the file to read from.

                using (StreamReader sr = File.OpenText(path))
                {
                    //   FlowDocument doc = new FlowDocument();
                    string[] s = File.ReadAllLines(path);
                    for (int i = 0; i < s.Length; i++)
                    {
                        Paragraph p = new Paragraph(new Run(s[i]));
                        doc.Blocks.Add(p);
                        //  TBoxInvoice.Text = TBoxInvoice.Text + "\r\n" + s[i];
                    }
                    //p.FontSize = 36;
                    //p = new Paragraph(new Run("The ultimate programming greeting!"));
                    //p.FontSize = 14;
                    //p.FontStyle = FontStyles.Italic;
                    //p.TextAlignment = TextAlignment.Left;
                    //p.Foreground = Brushes.Gray;
                    //doc.Blocks.Add(p);
                    fdViewer.Document = doc;
                }


            }
            catch (IOException er)
            {
                Console.WriteLine(er.StackTrace);
                MessageBoxResult result = MessageBox.Show("There is an Error in Opening or Finding the File!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    return;
                }

            }
        }

        //private void InvokePrint(object sender, RoutedEventArgs e)
        //{
        //    // Create the print dialog object and set options
        //    PrintDialog pDialog = new PrintDialog();
        //    pDialog.PageRangeSelection = PageRangeSelection.AllPages;
        //    pDialog.UserPageRangeEnabled = true;

        //    // Display the dialog. This returns true if the user presses the Print button.
        //    Nullable<Boolean> print = pDialog.ShowDialog();
        //    if (print == true)
        //    {
        //        XpsDocument xpsDocument = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
        //        FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
        //        pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
        //    }
        //}



        private void ButtonTotal_Click(object sender, RoutedEventArgs e)
        {
            LvShopping.Items.Clear();
            List<OrderList> list = db.GetAllOrderList();
            decimal total = 0.0m;
            decimal totalTax = 0.0m;
            foreach (OrderList l in list)
            {
                int categoryId = (l.ProductId - 1) / 6;
                int i = l.ProductId - categoryId * 6 - 1;
                string name = ProductName[categoryId, i];
                Counts[categoryId, i] = l.Quantity;
                decimal subtaotal = l.Quantity * l.UnitPrice;
                total = total + subtaotal;
                decimal tax = subtaotal * 0.15m;
                totalTax = totalTax + tax;
                Shopping s = new Shopping(l.ProductId, name, l.Quantity, l.UnitPrice, l.Discount, subtaotal, tax);
                LvShopping.Items.Add(s);
            }
            totalTaxCost.Text = String.Format("{0:C}", totalTax);
            BalancePriceTb.Text = String.Format("{0:C}", total);
            PaidTextBox.Text = String.Format("{0:C}", total);

        }

        private void ButtonInvoice_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;
            doc.PageHeight = pd.PrintableAreaHeight;
            doc.PageWidth = pd.PrintableAreaWidth;
            IDocumentPaginatorSource idocument = doc as IDocumentPaginatorSource;
            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int Id = 1;
            db.DeleteOrderById(Id);
            RefreshShoppingList();

        }

        private void LvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonAdd_Click(null, null);
        }

        private void LvShopping_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                ButtonDelete_Click(null, null);
            }

        }


    }
}
