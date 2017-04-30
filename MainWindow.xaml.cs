
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
        public Database db;
        public int IdOfCategory;
        public int MaxOfCategoryId;
        public int currentOrderId;
        public decimal total;
        decimal totalTax;
        private List<InStock> productList;
        public int[] QCounts = new int[64];
        FlowDocument doc = new FlowDocument();
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
            ReadAllProduct();
            RefreshShoppingList();
        }

        private void ReadAllProduct()
        {
            productList = db.GetAllProducts();
            currentOrderId = db.MaxOrderId() + 1;
            for (int i = 0; i < 64; i++)
            {
                QCounts[i] = 0;
            }
        }

        private void RefreshShoppingList()
        {
            LvShopping.Items.Clear();
            List<OrderList> list = db.GetOrderListbyId(currentOrderId);
            if (list != null)
            {
                total = 0.0m;
                totalTax = 0.0m;
                foreach (OrderList l in list)
                {
                    foreach (InStock ins in productList)
                    {
                        if (l.ProductId == ins.Id)
                        {
                            string name = ins.ProductName;
                            decimal subtotal = l.Quantity * l.UnitPrice;
                            total = total + subtotal;
                            decimal tax = subtotal * 0.15m;
                            totalTax = totalTax + tax;
                            Shopping s = new Shopping(l.ProductId, name, l.Quantity, l.UnitPrice, l.Discount, subtotal, tax);
                            LvShopping.Items.Add(s);
                        }
                    }
                }
            }
            SubTotal();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //List<OrderList> oList = new List<OrderList>();
            int idx = LvItems.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Please select Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ItemList it = (ItemList)LvItems.Items[idx];
            int quality = QCounts[it.ProductId - 1] + 1;
            QCounts[it.ProductId - 1] = quality;
            Console.WriteLine(" before add Olist"+ it.ProductId+"  "+ quality);
            OrderList ol = new OrderList(currentOrderId, it.ProductId, quality, it.Price, 0.1m);
            
            if (quality == 1)
            {
                db.AddOrderList(ol);
                Console.WriteLine(" after add 1 in Olist" + it.ProductId + "  " + quality);
                RefreshShoppingList();
                SubTotal();
                return;
            }
            db.UpdateOrderList(ol);
            Console.WriteLine(" after add >1 in Olist" + it.ProductId + "  " + quality);
            SubTotal();
            RefreshShoppingList();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 0;
            db.DeleteOrderById(currentOrderId);
            db.DeleteOrderListByOrderId(currentOrderId);
            RefreshShoppingList();
            NewClear();
            // LvItems.Items.Clear();

        }

        private void NewClear()
        {
            BalancePriceTb.Text = "";
            totalTaxCost.Clear();
            PaidTextBox.Clear();
            BalancePriceTb.Clear();
            for (int i = 0; i < 64; i++)
            {
                QCounts[i] = 0;
            }
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
            try
            {
                if (s.Quantity > 1)
                {
                    QCounts[s.ID - 1] = s.Quantity - 1;
                    OrderList o = new OrderList(currentOrderId, s.ID, QCounts[s.ID - 1], s.UnitPrice, 0.1m);
                    db.UpdateOrderList(o);
                    RefreshShoppingList();
                    SubTotal();
                    return;
                }
                db.DeleteOrderListById(s.ID);
                QCounts[s.ID - 1] = 0;
                RefreshShoppingList();
                SubTotal();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Database query error " + ex.Message);
            }
        }

        private void ButtonChekOut_Click(object sender, RoutedEventArgs e)
        {
            if (total == 0m)
            {
                return;
            }
            doc.Blocks.Clear();
            string theDate = DpDate.Text;
            //.ToString("yyyy-MM-dd");
            string itemPurchasedInfo = "";
            string invoiceNoText = Convert.ToString(DpDate.SelectedDate.Value.Year) + Convert.ToString(DpDate.SelectedDate.Value.Month)
                                   + Convert.ToString(DpDate.SelectedDate.Value.Day) + Convert.ToString(currentOrderId);
            int invoiceNo;
            Int32.TryParse(invoiceNoText, out invoiceNo);
            itemPurchasedInfo = "=============================" + "\r\n" + "Mike & Elmira's Company" + "\r\n"
                                + "=============================" + "\r\n" + "" + "Address:" + "\r\n" +
                                "John Abbot College" + "\r\n"
                                + "Phone: 514- 543 74 89" + "\r\n" + "INVOICE NO: " + invoiceNo + "\t\t" +
                                "Date: " + theDate + "\r\n"
                                + "=============================" + "\r\n";
            for (int i = 0; i < LvShopping.Items.Count; i++)
            {
                Shopping s = (Shopping)LvShopping.Items[i];
                itemPurchasedInfo += "Product Name: " + s.ProductName + "\r\n  Quantity: " + s.Quantity
                                     + "\r\n  Unit Price: " + String.Format("{0:C}", s.UnitPrice) + "\r\n\r\n";
            }
            itemPurchasedInfo += "====================" + "\r\n";
            itemPurchasedInfo += "Tax:  " + totalTaxCost.Text + "\r\n";
            itemPurchasedInfo += "Balance:  " + BalancePriceTb.Text + "\r\n";
            itemPurchasedInfo += "Paid:  " + PaidTextBox.Text + "\r\n";
            itemPurchasedInfo += "Method Of Payment:  " + ComboCard.Text;
            itemPurchasedInfo += "\r\n" + "*****************************" + "\r\n"
                                 + "Thank you for Shoping at Mike & Elmira's Company";
            Paragraph p = new Paragraph(new Run(itemPurchasedInfo));
            doc.Blocks.Add(p);
            FdViewer.Document = doc;
            Order o = new Order(currentOrderId, 1, DpDate.SelectedDate.Value.Date, 100, total, ComboCard.Text, invoiceNo);
            db.AddOrder(o);
            DeductProduct();
            currentOrderId++;
            TabControl.SelectedIndex = 1;
            total = 0.0m;
        }

        private void DeductProduct()
        {
            for (int i = 0; i < LvShopping.Items.Count; i++)
            {
                Shopping s = (Shopping)LvShopping.Items[i];
                db.DeductProductById(s.ID, s.Quantity);
                if (db.ProductStockById(s.ID))
                {
                    MessageBox.Show(s.ProductName + " needs reload !!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        public void SubTotal()
        {
            List<OrderList> list = db.GetOrderListbyId(currentOrderId);
            total = 0.0m;
            totalTax = 0.0m;
            foreach (OrderList l in list)
            {
                int categoryId = (l.ProductId - 1) / 6;
                int i = l.ProductId - categoryId * 6 - 1;
                //string name = ProductName[categoryId, i];
                //Counts[categoryId, i] = l.Quantity;
                decimal subtotal = l.Quantity * l.UnitPrice;
                total = total + subtotal;
                decimal tax = subtotal * 0.15m;
                totalTax = totalTax + tax;
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
            db.DeleteOrderById(currentOrderId);
            db.DeleteOrderListByOrderId(currentOrderId);
            // RefreshShoppingList();
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

        private void ItemList(int categoryId)
        {
           // List<InStock> productList = db.GetAllProducts();
            LvItems.Items.Clear();
            foreach (InStock ins in productList)
            {
                if (ins.CategoryId == categoryId)
                {
                    ItemList it = new ItemList(ins.ProductName, ins.SalePrice, ins.Id);
                    LvItems.Items.Add(it);
                }
            }
            LvItems.Items.Refresh();
            TabControl.SelectedIndex = 0;
        }

        private void ButtonBeer_Click(object sender, RoutedEventArgs e)
        {
            ItemList(1);
        }
        private void ButtonDessert_Click(object sender, RoutedEventArgs e)
        {
            ItemList(2);
        }
        private void ButtonLunch_Click(object sender, RoutedEventArgs e)
        {
            ItemList(3);
        }
        private void ButtonHotDrink_Click(object sender, RoutedEventArgs e)
        {
            ItemList(4);
        }
        private void ButtonDinner_Click(object sender, RoutedEventArgs e)
        {
            ItemList(5);
        }
        private void ButtonWine_Click(object sender, RoutedEventArgs e)
        {
            ItemList(6);
        }

        
    }
}
