using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleManagementSys
{
    public class OrderList
    {
        private int _orderId;
        private int _productId;
        private int _quantity;
        private decimal _unitPrice;
        private decimal _discount;

        public OrderList(int orderId, int productId, int quantity, decimal unitPrice, decimal discount)
        {
            _orderId = orderId;
            _productId = productId;
            _quantity = quantity;
            _unitPrice = unitPrice;
            _discount = discount;
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
    }

}
