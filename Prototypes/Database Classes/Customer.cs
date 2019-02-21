using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Customer
    {
        public Customer(string title, string forname, string surname, string country, string addressline1, string addressline2, string town, string county, string postcode, string telephonenum, string email)
        {
            this.Title = title;
            this.Forname = forname;
            this.Surname = surname;
            this.Country = country;
            this.AddressLine1 = addressline1;
            this.AddressLine2 = addressline2;
            this.Town = town;
            this.County = county;
            this.Postcode = postcode;
            this.TelephoneNum = telephonenum;
            this.Email = email;

        }

        public string Title { get; set; }
        public string Forname { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string TelephoneNum { get; set; }
        public string Email { get; set; }
    }
}
