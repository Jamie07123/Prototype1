using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Basket
    {
        public Basket(int orderid, int itemid, int quantity, string retailprice)
        {
            this.ItemId = itemid;
            this.OrderId = orderid;
            this.Quantity = quantity;
            this.RetailPrice = retailprice;
        }

        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string RetailPrice { get; set; }
    }
}
