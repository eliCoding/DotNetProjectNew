﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    public class globas
    {
        //public static User CurrentUser;
        //public static Portoflio CurrentPortfolio;
        private static Database _db;
        public static Database Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new Database();
                }
                return _db;
            }
        }

    }

}
