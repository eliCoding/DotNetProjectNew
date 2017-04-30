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
            if (firstName == null) throw new ArgumentNullException("firstName");
            if (lastName == null) throw new ArgumentNullException("lastName");
            if (userName == null) throw new ArgumentNullException("userName");
            if (pSword == null) throw new ArgumentNullException("pSword");
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
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string PSword
        {
            get { return _pSword; }
            set { _pSword = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }
    }
}
