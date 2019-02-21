using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_Library
{
    public class Item
    {
        public Item(string sku, string itemname, string barcode, string retailprice, string purchaseprice, string stocklvl, string description)
        {
            this.SKU = sku;
            this.ItemName = itemname;
            this.Barcode = barcode;
            this.RetailPrice = retailprice;
            this.PurchasePrice = purchaseprice;
            this.StockLvl = stocklvl;
            this.Description = description;
        }

        public string SKU { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public string RetailPrice { get; set; }
        public string PurchasePrice { get; set; }
        public string StockLvl { get; set; }
        public string Description { get; set; }
    }
}
