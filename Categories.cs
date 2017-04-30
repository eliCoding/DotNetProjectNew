
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
            set
            {
                if (value.Length < 2 || value.Length > 50 || value.Equals(""))
                {
                    throw new ArgumentException("Category Name must be 2-50 characters long");
                }
                _categoryName = value;
            }
        }
       
    }
}
