using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Order
    {
        public Order(int customerid, DateTime date, string status)
        {
            this.CustomerId = customerid;
            this.Date = date;
            this.Status = status;
        }

        public int CustomerId { get; set; }
        public DateTime Date { get; set;}
        public string Status { get; set; }

    }
}