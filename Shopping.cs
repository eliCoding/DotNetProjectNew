using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    public class Shopping
    {
        private int _id;
        private string _productName;
        private int _quantity;
        private decimal _unitPrice;
        private decimal _discount;
        private decimal _total;
        private decimal _tax;

        public Shopping(int id, string productName, int quantity, decimal unitPrice, decimal discount, decimal total, decimal tax)
        {
            this.ID = id;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Discount = discount;
            this.Total = total;
            this.Tax = tax;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }


        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }


        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }


        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }


        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }


    }
}