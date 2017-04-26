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
        private Database db;
        // private PrintInvoice pi;
        public int IdOfCategory;
        public int currentOrderId;
        decimal total;
        decimal totalTax;

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

        List<Shopping> shoppingList = new List<Shopping>();
        FlowDocument doc = new FlowDocument();
        public MainWindow()
        {
            //   pi = new PrintInvoice(); 
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
            currentOrderId = db.MaxOrderId() + 1;
        }

        private void RefreshShoppingList()
        {
            LvShopping.Items.Clear();
            List<OrderList> list = db.GetAllOrderList(currentOrderId);
            if (list != null)
            {
                total = 0.0m;
                totalTax = 0.0m;
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
            OrderList ol = new OrderList(currentOrderId, productId, quantity, unitprice, 0.1m);
            if (quantity == 1)
            {
                db.AddOrderList(ol);
                RefreshShoppingList();
                SubTotal();
                return;
            }
            db.UpdateOrderList(ol);
            SubTotal();
            RefreshShoppingList();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 0;
            db.DeleteOrderById(currentOrderId);
            NewClear();
        }

        private void NewClear()
        {
            BalancePriceTb.Text = "";
            totalTaxCost.Clear();
            PaidTextBox.Clear();
            BalancePriceTb.Clear();
            //TabControl.SelectedIndex = 0;
            ReadProductPrice();
            RefreshShoppingList();
            total = 0.0m;
            totalTax = 0.0m;
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
                OrderList o = new OrderList(currentOrderId, s.ID, s.Quantity - 1, s.UnitPrice, 0.1m);
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
            if (total==0m)
            {
                return;
            }
            doc.Blocks.Clear();
            string theDate = dpDate.Text;
            //.ToString("yyyy-MM-dd");
            string itemPurchasedInfo = "";
            string invoiceNoText = Convert.ToString(dpDate.SelectedDate.Value.Year) + Convert.ToString(dpDate.SelectedDate.Value.Month)
                + Convert.ToString(dpDate.SelectedDate.Value.Day) + Convert.ToString(currentOrderId);
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
            Order o = new Order(currentOrderId, 1, dpDate.SelectedDate.Value.Date, 100, total, ComboCard.Text, invoiceNo);
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
                    MessageBox.Show(s.ProductName+ " needs reload !!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }
        private void SubTotal()
        {
            List<OrderList> list = db.GetAllOrderList(currentOrderId);
            total = 0.0m;
            totalTax = 0.0m;
            foreach (OrderList l in list)
            {
                int categoryId = (l.ProductId - 1) / 6;
                int i = l.ProductId - categoryId * 6 - 1;
                string name = ProductName[categoryId, i];
                Counts[categoryId, i] = l.Quantity;
                decimal subtotal = l.Quantity * l.UnitPrice;
                total = total + subtotal;
                decimal tax = subtotal * 0.15m;
                totalTax = totalTax + tax;
            }
            totalTaxCost.Text = String.Format("{0:C}", totalTax);
            BalancePriceTb.Text = String.Format("{0:C}", total);
            PaidTextBox.Text = String.Format("{0:C}", total);
        }

        private void ButtonInventory_Click(object sender, RoutedEventArgs e)
        {
            //ManagementConsole.MainWindow f = new ManagementConsole.MainWindow();
            //f.Show();
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

        private void Button_NewOrder(object sender, RoutedEventArgs e)
        {
            NewClear();
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
            TabControl.SelectedIndex = 0;
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


     

       

      
            }
        }

   

   //     public Point startPoint;
   //     private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
   //     {
   //         // Store the mouse position
   //        startPoint = e.GetPosition(null);
   //     }

   //     private void List_MouseMove(object sender, MouseEventArgs e)
   //     {
   //         // Get the current mouse position
   //         Point mousePos = e.GetPosition(null);
   //         Vector diff = startPoint - mousePos;

   //         if (e.LeftButton == MouseButtonState.Pressed &&
   //             Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
   //             Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
   //         {
   //             // Get the dragged ListViewItem
   //             ListView listView = sender as ListView;
   //             ListViewItem listViewItem =
   //                 FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

   //             // Find the data behind the ListViewItem
   //ItemList contact = (ItemList)listView.ItemContainerGenerator.
   //ItemFromContainer(listViewItem);

   //             // Initialize the drag & drop operation
   //     DataObject dragData = new DataObject("myFormat", contact);
   //             //DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
   //         }
   //     }


   //     private static T FindAnchestor<T>(DependencyObject current)
   //         where T : DependencyObject
   //     {
   //         do
   //         {
   //             if (current is T)
   //             {
   //                 return (T)current;
   //             }
   //             current = VisualTreeHelper.GetParent(current);
   //         }
   //         while (current != null);
   //         return null;
   //     }

   //     private void DropList_DragEnter(object sender, DragEventArgs e)
   //     {
   //         if (!e.Data.GetDataPresent("myFormat") ||
   //             sender == e.Source)
   //         {
   //             e.Effects = DragDropEffects.None;
   //         }
   //     }

   //     private void DropList_Drop(object sender, DragEventArgs e)
   //     {
   //         if (e.Data.GetDataPresent("myFormat"))
   //         {
   //             ItemList contact = e.Data.GetData("myFormat") as ItemList;
   //             ListView listView1 = sender as ListView;
   //            // listView.Items.Add(contact);
   //         }
   //     }


