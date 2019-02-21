using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Supplier
    {
        public Supplier(string suppname, string suppcontact, string suppaddress)
        {
            this.SuppName = suppname;
            this.SuppContact = suppcontact;
            this.SuppAddress = suppaddress;
        }

        public string SuppName { get; set; }
        public string SuppContact { get; set; }
        public string SuppAddress { get; set; }
    }
}
