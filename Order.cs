using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    public class Order
    {
        private int _orderId;
        private int _empId;
        private DateTime _orderDate;
        private int _customerId;
        private decimal _totalPrice;
        private string _paymentMethod;
        private int _invoiceNr;

        public Order(int orderId, int empId, DateTime orderDate, int customerId, decimal totalPrice, string paymentMethod, int invoiceNr)
        {
            _orderId = orderId;
            _empId = empId;
            _orderDate = orderDate;
            _customerId = customerId;
            _totalPrice = totalPrice;
            _paymentMethod = paymentMethod;
            _invoiceNr = invoiceNr;
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int EmpId
        {
            get { return _empId; }
            set { _empId = value; }
        }
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
        }
        public int InvoiceNr
        {
            get { return _invoiceNr; }
            set { _invoiceNr = value; }
        }
    }
}

