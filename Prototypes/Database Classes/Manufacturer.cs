using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Manufacturer
    {
        public Manufacturer(string manuaddress, string manucontact, string manuname)
        {
            this.ManuAddress = manuaddress;
            this.ManuContact = manucontact;
            this.ManuName = manuname;
        }

        public string ManuAddress { get; set; }
        public string ManuContact { get; set; }
        public string ManuName { get; set; }
    }
}
