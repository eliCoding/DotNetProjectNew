﻿using System;
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
            TabControlConsole.SelectedIndex = 0;
            TabControlSearch.SelectedIndex = 0;
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
                foreach (Employee emp in list)
                {
                    LvEmployee.Items.Add(emp);
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
                        MessageBox.Show("Please select an Employee", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        MessageBox.Show("Please select a Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    InStock ins = (InStock)LvItems.Items[LvItems.SelectedIndex];
                    db1.DeleteProductById(ins.Id);
                    ReadStocks();
                    break;
            }

        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (TabControlConsole.SelectedIndex)
            {
                case 2:
                    if (LvEmployee.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select an Employee", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Employee ep = (Employee)LvEmployee.Items[LvEmployee.SelectedIndex];
                    TbEmployeeId.Text = ep.Id + "";
                    TbFirstName.Text = ep.FirstName;
                    TbLastName.Text = ep.LastName;
                    TbUserName.Text = ep.UserName;
                    TbPassWord.Text = ep.PSword;
                    TbSalary.Text = ep.Salary+"";
                    TabControlSearch.SelectedIndex = 2;
                    break;
                case 1:
                    if (LvOrders.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Order Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Order o = (Order)LvOrders.Items[LvOrders.SelectedIndex];
                    TbOrderID.Text = o.OrderId+"";
                    TbInvoiceNo.Text = o.InvoiceNr+"";
                    TbCustomerId.Text = o.CustomerId + "";
                    TbEmployeeID.Text = o.EmpId+"";
                    TbTotalPrice.Text = o.TotalPrice+"";
                    TbPaymentMethod.Text = o.PaymentMethod+"";
                    TbOderDate.Text = o.OrderDate.ToString();
                    TabControlSearch.SelectedIndex = 1;
                    break;
                case 0:
                    if (LvItems.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    InStock ins = (InStock)LvItems.Items[LvItems.SelectedIndex];
                    TbProductName.Text = ins.ProductName;
                    TbAlertUnit.Text = ins.TriggerLevel+"";
                    TbVendor.Text  = ins.Vendor;
                    TbCategoryId.Text  = ins.CategoryId+"";
                    TbStockUnit.Text  = ins.Quantity+"";
                    TbPurchasedPrice.Text  = ins.UnitPrice+"";
                    TbProductId.Text = ins.Id+"";
                    break;
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            switch (TabControlSearch.SelectedIndex)
            {
                case 2:
                    int Id;
                    Int32.TryParse(TbEmployeeId.Text, out Id);
                    decimal salary;
                    decimal.TryParse(TbSalary.Text, out salary);
                    Employee ep = new Employee(Id, TbFirstName.Text, TbLastName.Text, TbUserName.Text, TbPassWord.Text, salary);
                    List<Employee> list = db1.GetAllEmployees();
                    if (list != null)
                    {
                        foreach (Employee emp in list)
                        {
                            if (emp.Id == Id)
                            {
                                db1.UpdateEmployee(ep);
                                ReadEmployee();
                                return;
                            } 
                        }
                        db1.AddEmployee(ep);
                        ReadEmployee();
                    }
                    return;
                case 1:
                    int orderId;
                    Int32.TryParse(TbOrderID.Text, out orderId);
                    decimal totalPrice;
                    decimal.TryParse(TbTotalPrice.Text, out totalPrice);
                    int invoiceNr;
                    Int32.TryParse(TbInvoiceNo.Text, out invoiceNr);
                    int customerId;
                    Int32.TryParse(TbCustomerId.Text , out customerId);
                    int empId;
                    Int32.TryParse(TbEmployeeID.Text, out empId);
                    string paymentMethod = TbPaymentMethod.Text;
                    DateTime orderDate;
                    DateTime.TryParse(TbOderDate.Text, out orderDate);
                    Order od = new Order(orderId, empId, orderDate.Date, customerId, totalPrice,paymentMethod,invoiceNr);
                   // Order o = new Order(currentOrderId, 1, dpDate.SelectedDate.Value.Date, 100, total, ComboCard.Text, invoiceNo);



                    List<Order> oList = db1.GetAllOrders();
                    if (oList != null)
                    {
                        foreach (Order o in oList)
                        {
                            if (o.OrderId == orderId)
                            {
                                db1.UpdateOrder(od);
                                ReadOrders();
                                return;
                            } 
                        }
                        db1.AddOrder(od);
                        ReadOrders();
                    }
                    return;
                case 0:
                    if (LvItems.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Item", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    InStock ins = (InStock)LvItems.Items[LvItems.SelectedIndex];
                    db1.DeleteProductById(ins.Id);
                    ReadStocks();
                    break;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (TabControlConsole.SelectedIndex)
            {
                case 2:
                    TbEmployeeId.Text = "";
                    TbFirstName.Text = "";
                    TbLastName.Text = "";
                    TbUserName.Text = "";
                    TbPassWord.Text = "";
                    TbSalary.Text = "";
                    TabControlSearch.SelectedIndex = 2;
                    break;
                case 1:
                    TbOrderID.Text = "";
                    TbInvoiceNo.Text = "";
                    TbCustomerId.Text = "";
                    TbEmployeeID.Text = "";
                    TbTotalPrice.Text = "";
                    TbPaymentMethod.Text = "";
                    TbOderDate.Text = "";
                    TabControlSearch.SelectedIndex = 1;
                    break;
                case 0:

                    TbProductName.Text = "";
                    TbAlertUnit.Text = "";
                    TbVendor.Text = "";
                    TbCategoryId.Text = "";
                    TbStockUnit.Text = "";
                    TbPurchasedPrice.Text = "";
                    TbProductId.Text = "";
                    TabControlSearch.SelectedIndex = 0;
                    break;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbOrderID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
