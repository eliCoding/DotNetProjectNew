using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
   public class ItemList
    {
        string productName;
        decimal price;
        private int productId;

        public ItemList(string productName, decimal price, int productId)
        {
          
            this.productName = productName;
            this.price = price;
            this.productId = productId;
        }

        public string ProductName
        {
            get { return productName; }
              set
            {
                if (value.Length < 2 || value.Length > 50 || value.Equals(null))
                {
                    throw new ArgumentException("Product Name must be 2-50 characters long");
                }
                productName = value;
            }
         //   set { productName = value; }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value.Equals(null) || value < 0)
                {
                    throw new ArgumentException("Please enter  positive Number");
                }
                price = value;
            }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
    }
}
