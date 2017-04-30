using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleManagementSys;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PointOfSaleManagementSys.Tests
{
    [TestClass()]
    public class POSTests
    {
        [TestMethod()]
        public void CategoriesNameNullTest()
        {

            Categories c = new Categories(1, "Dessert");
            Boolean exception = false;
            try
            {
                c.CategoryName.Equals(null);

            }
            catch (ArgumentException e)
            {
                exception = true;

            }
            catch (NullReferenceException e)
            {
                Assert.Fail("CategoryName(null) must throw IllegalArgumentException, not NullPointerException");
            }

            // Assert.IsNotNull(expected, c.CategoryName);
            Assert.AreEqual(exception, false);



        }
         [TestMethod()]
        public void TestNameLongEnoughCategory() {

         Categories c = new Categories(1, "Dessert");
        String name = "M";
        try {
            for (int i = 2; i <= 50; i++) {
                name += "a";
             
                c.CategoryName.Equals(name);
            }
        } catch (ArgumentException e) {
            Assert.Fail("setName(\"" + name + "\") should not throw ArgumentException");
        }
    }

        [TestMethod()]
        public void EmployeeNullTest()
        {

            Employee e = new Employee(1, "JOhn", "Smith", "JOhn", "7000", 65);
            Boolean exception = false;
            try
            {
                e.FirstName.Equals(null);

                e.LastName.Equals(null);
                e.UserName.Equals(null);
                e.PSword.Equals(null);
                e.Salary.Equals(null);

            }
            catch (ArgumentException ex)
            {
                exception = true;

            }
            catch (NullReferenceException ex)
            {
                Assert.Fail("Null must throw IllegalArgumentException, not NullPointerException");
            }


            Assert.AreEqual(exception, false);
        }
        [TestMethod()]
        public void TestNameLongEnoughEmployee()
        {

            Employee e = new Employee(1, "JOhn", "Smith", "JOhn", "7000", 65);
            String name = "M";
            try
            {
                for (int i = 2; i <= 50; i++)
                {
                    name += "a";
                    e.FirstName.Equals(name);
                    e.LastName.Equals(name);
                    e.PSword.Equals(name);
                    e.UserName.Equals(name);
                }
            }
            catch (ArgumentException ea)
            {
                Assert.Fail("Name(\"" + name + "\") should not throw ArgumentException");
            }
        }

        [TestMethod()]
        public void InStockNullTest()
        {

            InStock i = new InStock(1, 2, "cofee", 3, 4, 5, 6, "", DateTime.Now);
            Boolean exception = false;
            try
            {
                i.ProductName.Equals(null);

                i.UnitPrice.Equals(null);
                i.SalePrice.Equals(null);
                i.TriggerLevel.Equals(null);
                i.Vendor.Equals(null);
                i.ExpiryDate.Equals(null);

            }
            catch (ArgumentException ex)
            {
                exception = true;
                Assert.Fail("Shoud not be null");
            }



            Assert.AreEqual(exception, false);
        }
        [TestMethod()]
        public void TestNameLongEnoughINstock()
        {

            InStock s = new InStock(1, 2, "cofee", 3, 4, 5, 6, "", DateTime.Now);
            String name = "M";
            try
            {
                for (int i = 2; i <= 50; i++)
                {
                    name += "a";
                    s.ProductName.Equals(name);
              

                }
            }
            catch (ArgumentException e)
            {
                Assert.Fail("Name(\"" + name + "\") should not throw ArgumentException");
            }
        }

        [TestMethod()]
        public void OrderListNullTest()
        {
            InStock i = new InStock(1, 2, "cofee", 3, 4, 5, 6, "", DateTime.Now);
            Boolean exception = false;
            try
            {
                i.ProductName.Equals(null);

                i.UnitPrice.Equals(null);
                i.SalePrice.Equals(null);
                i.TriggerLevel.Equals(null);
                i.Vendor.Equals(null);
                i.ExpiryDate.Equals(null);

            }
            catch (ArgumentException ex)
            {
                exception = true;
                Assert.Fail("Shoud not be null");
            }



            Assert.AreEqual(exception, false);
        }

        [TestMethod()]
        public void ItemListNullTest()
        {

            ItemList e = new ItemList("Coffee", 4, 1);
            Boolean exception = false;
            try
            {
                e.ProductName.Equals(null);

                e.Price.Equals(null);
                e.ProductId.Equals(null);


            }
            catch (ArgumentException ex)
            {
                exception = true;
                Assert.Fail("Null must throw IllegalArgumentException, not NullPointerException");

            }


            Assert.AreEqual(exception, false);
        }

        [TestMethod()]
        public void ShoppingNUllTest()
        {


            Shopping s = new Shopping(1, "cofee", 3, 4, 3, 5, 1);
            Boolean exception = false;
            try
            {
                s.ID.Equals(null);
                s.ProductName.Equals(null);
                s.Quantity.Equals(null);
                s.UnitPrice.Equals(null);
                s.Discount.Equals(null);
                s.Total.Equals(null);
                s.Tax.Equals(null);




            }
            catch (ArgumentException ex)
            {
                exception = true;
                Assert.Fail("Null must throw IllegalArgumentException, not NullPointerException");

            }


            Assert.AreEqual(exception, false);
        }

        [TestMethod()]
        public void OrderNUllTest()
        {
            //public Order(int orderId, int empId, DateTime orderDate, int customerId, decimal totalPrice, string paymentMethod, int invoiceNr)
            Order s = new Order(1, 2, DateTime.Now, 2, 3, "CASH", 23);
            Boolean exception = false;
            try
            {
                s.OrderId.Equals(null);
                s.EmpId.Equals(null);
                s.OrderDate.Equals(null);
                s.CustomerId.Equals(null);
                s.TotalPrice.Equals(null);
                s.PaymentMethod.Equals(null);

            }
            catch (ArgumentException ex)
            {
                exception = true;
                Assert.Fail("Null must throw IllegalArgumentException, not NullPointerException");

            }


            Assert.AreEqual(exception, false,"Name(null) must throw exception");
        }

       
    }
}
