using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PointOfSaleManagementSys
{
    public class InStock
    {
        private int _id;
        private int _categoryId;
        private string _productName;
        private decimal _unitPrice;
        private decimal _salePrice;
        private int _quantity;
        private int _triggerLevel;
        private string _vendor;
        private DateTime _expiryDate;

        //  private string _vendorAddress;
        public InStock(int id, int categoryId, string productName, decimal unitPrice, decimal salePrice, int quantity,
            int triggerLevel, string vendor, DateTime expiryDate)
        {
           
            _id = id;
            _categoryId = categoryId;
            _productName = productName;
            _unitPrice = unitPrice;
            _salePrice = salePrice;
            _quantity = quantity;
            _triggerLevel = triggerLevel;
            _vendor = vendor;
            _expiryDate = expiryDate;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public string ProductName
        {
            get { return _productName; }
              set
            {
                if (value.Length < 2 || value.Length > 50 || value.Equals(null))
                {
                    throw new ArgumentException("Product Name must be 2-50 characters long");
                }
                _productName = value;
            }
           // set {  = value; }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value.Equals(null) || value < 0)
                {
                    throw new ArgumentException("Please enter  positive Number");
                }
                _unitPrice = value;
            }
            //   set { _unitPrice = value; }
        }

        public decimal SalePrice
        {
            get { return _salePrice; }
            set
            {
                if (value.Equals(null) || value < 0)
                {
                    throw new ArgumentException("Please enter  positive Number");
                }
                _salePrice = value;
            }
            //set { _salePrice = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public int TriggerLevel
        {
            get { return _triggerLevel; }
            set { _triggerLevel = value; }
        }

        public string Vendor
        {
            get { return _vendor; }
            set
            {
                if (value.Length < 2 || value.Length > 50 || value.Equals(null))
                {
                    throw new ArgumentException("Vendor must be 2-50 characters long");
                }
                _vendor = value;
            }
            // set { _vendor = value; }
        }

        public DateTime ExpiryDate
        {
            get { return _expiryDate; }
            set
            {

                if (value < DateTime.Now)
                {
                    throw new ArgumentException("expiry Date Has to be After Current Date");
                }
                _expiryDate = value;
            }
           // set { _expiryDate = value; }
        }

      
       
    }
}