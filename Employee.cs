using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PointOfSaleManagementSys
{

    public class Employee
    {
        private int _Id;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _pSword;
        private decimal _salary;

        public Employee(int id, string firstName, string lastName, string userName, string pSword, decimal salary)
        {
           // if (firstName == null) throw new ArgumentNullException("firstName");
           // if (lastName == null) throw new ArgumentNullException("lastName");
           // if (userName == null) throw new ArgumentNullException("userName");
          //  if (pSword == null) throw new ArgumentNullException("pSword");
            _Id = id;
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;
            _pSword = pSword;
            _salary = salary;
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
             set
            {
                if (value.Length < 2 || value.Length > 50 || value.Equals(""))
                {
                    throw new ArgumentException("First Name must be 2-50 characters long");
                }
                _firstName = value;
            }
           
        }

        public string LastName
        {
            get { return _lastName; }
            set {
                if (value.Length < 2 || value.Length > 50 || value.Equals(""))
                {
                    throw new ArgumentException("Last Name must be 2-50 characters long");
                }
                _lastName = value;
            }
           
           
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value.Length < 2 || value.Length > 15 || value.Equals(""))
                {
                    throw new ArgumentException("User Name must be 2-15 characters long");
                }
                 _userName = value;
            }
            //set { _userName = value; }
        }

        public string PSword
        {
            get { return _pSword; }
            set
            {
                if (value.Length < 2 || value.Length > 10 || value.Equals(""))
                {
                    throw new ArgumentException("Password must be 2-10 characters long");
                }
                _pSword = value;
            }

            //set { _pSword = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
             set
            {
                if ( value.Equals("") || value < 0 )
                {
                    throw new ArgumentException("Please enter  positive Number");
                }
                _salary = value;
            }
           
        }
    }
}
