using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    class ItemList
    {
        string productName;
        decimal price;
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

          public ItemList(string productName, decimal price)
        {
            this.ProductName= productName;
            this.Price= price;

        }



    }
}
