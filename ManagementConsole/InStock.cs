using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementConsole
{
    class InStock
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _categoryId;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        private decimal _salePrice;

        public decimal SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = value; }
        }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private string _vendor;

        public string Vendor
        {
            get { return _vendor; }
            set { _vendor = value; }
        }
        private string _vendorAddress;

        public string VendorAddress
        {
            get { return _vendorAddress; }
            set { _vendorAddress = value; }
        }
        private DateTime _expiryDate;

        public DateTime ExpiryDate
        {
            get { return _expiryDate; }
            set { _expiryDate = value; }
        }
        public InStock(int id,int categoryId ,string productName, decimal unitPrice, decimal salePrice, int quantity ,string vendor, string vendorAddress, DateTime expiryDate)
        {
           this.Id = id;
           this.CategoryId = categoryId;
           this.ProductName = productName;
           this.UnitPrice = unitPrice;
           this.SalePrice = salePrice;
           this.Quantity = quantity;
           this.Vendor =vendor;
           this.VendorAddress = vendorAddress;
           this.ExpiryDate = expiryDate;
          
        }

    }
}
