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
            if (productName == null) throw new ArgumentNullException("productName");
            this.productName = productName;
            this.price = price;
            this.productId = productId;
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
    }
}
