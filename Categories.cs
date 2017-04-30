
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    public class Categories
    {
        private int _categoryId;
        private string _categoryName;

        public Categories(int id, string categoryName)
        {
            this.CategoryId = id;
            this.CategoryName = categoryName;

        }
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
       

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
       
    }
}
