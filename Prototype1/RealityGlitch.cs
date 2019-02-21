using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype_Library;

namespace Prototype1
{
    public partial class RealityGlitch : Form
    {
        CRUD crud = new CRUD();
        int custIdSelected;
        int itemIdSelected;
        int orderId;
        public RealityGlitch()
        {
            InitializeComponent();
            titlebox.Text = "Mr";   //Sets title combobox default value
            //Sets all datagridviews to display all values on startup//
            UpdateItemDataGrid("");
            UpdateCustomerDataGrid("");
            UpdateCustomerGrid("");
            UpdateItemGrid("");
            //-------------------------------------------------------//
            orderId = -1; //Clears the orderID on startup
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //MENU/BACK BUTTONS//
        //-----------------------------------------------------------------------------------------------------------------//
        private void menu_customer_Click(object sender, EventArgs e)
        {
            customer_panel.Visible = true;
        }

        private void customer_back_Click(object sender, EventArgs e)
        {
            customer_panel.Visible = false;
        }

        private void item_back_Click(object sender, EventArgs e)
        {
            item_panel.Visible = false;
        }

        private void menu_item_Click(object sender, EventArgs e)
        {
            item_panel.Visible = true;
        }

        private void manufacturer_back_Click(object sender, EventArgs e)
        {
            manufacturer_panel.Visible = false;
        }

        private void menu_manufacturer_Click(object sender, EventArgs e)
        {
            manufacturer_panel.Visible = true;
        }

        private void supplier_back_Click(object sender, EventArgs e)
        {
            supplier_panel.Visible = false;
        }

        private void menu_Supplier_Click(object sender, EventArgs e)
        {
            supplier_panel.Visible = true;
        }
        //-----------------------------------------------------------------------------------------------------------------//
        //SUBMIT BUTTONS//
        //-----------------------------------------------------------------------------------------------------------------//
        private void Item_submit_Click(object sender, EventArgs e)
        {
            Item i = new Item(skubox.Text, itemtitlebox.Text, barcodebox.Text, retailbox.Text, purchasebox.Text, stockbox.Text, descbox.Text);
            crud.AddItem(i);
            UpdateItemDataGrid("");
        }

        private void Customer_submit_Click(object sender, EventArgs e)
        {
            Customer c = new Customer(titlebox.Text, fornamebox.Text, surnamebox.Text, countrybox.Text, address1box.Text, address2box.Text, townbox.Text, countybox.Text, postcodebox.Text, telebox.Text, emailbox.Text);
            crud.AddCustomer(c);
            UpdateCustomerDataGrid("");        }

        private void Manufacturer_submit_Click(object sender, EventArgs e)
        {
            Manufacturer m = new Manufacturer(manaddressbox.Text, mancontactbox.Text, mannamebox.Text);
            crud.AddManufacturer(m);
        }

        private void supplier_submit_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier(supnamebox.Text, supcontactbox.Text, supaddressbox.Text);
            crud.AddSupplier(s);
        }
        //-----------------------------------------------------------------------------------------------------------------//
        //Pulling IDs when selected in the datagridview//
        //-----------------------------------------------------------------------------------------------------------------//
        private void CustomerGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CustomerGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = CustomerGridView.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = CustomerGridView.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["CustomerId"].Value);

                custIdSelected = Convert.ToInt32(a);

                PopulateOrderGrid(custIdSelected);
            }
            
        }

        private void ItemGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (ItemGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = ItemGridView.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = ItemGridView.Rows[selectedrowindex];

                string i = Convert.ToString(selectedRow.Cells["ItemId"].Value);

                itemIdSelected = Convert.ToInt32(i);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //Creates new order and adds selected item to a basket//
        //-----------------------------------------------------------------------------------------------------------------//

        private void addItem_Click(object sender, EventArgs e)
        {
            int selectedrowindex = ItemGridView.SelectedCells[0].RowIndex;
            DataGridViewRow itemRow = ItemGridView.Rows[selectedrowindex];
            string retailprice = Convert.ToString(itemRow.Cells["RetailPrice"].Value);
            if (orderId == -1)
            {
                Order o = new Order(custIdSelected, DateTime.Now, "Processing");
                orderId = crud.AddOrder(o);
                Basket b = new Basket(orderId, itemIdSelected, Convert.ToInt32(itemQuantity.Text), retailprice);
                crud.AddBasket(b);
            }
            else
            {
                if(itemQuantity.Text == "")
                {
                    MessageBox.Show("ERROR - PLEASE ENETER ITEM QUANTITY");
                }
                else
                {
                    Basket b = new Basket(orderId, itemIdSelected, Convert.ToInt32(itemQuantity.Text), retailprice);
                    crud.AddBasket(b);
                }
            }
            PopulateOrderGrid(custIdSelected);
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //DatagridView update/search//
        //-----------------------------------------------------------------------------------------------------------------//

        //Customer datagridview on orders screen
        private void CustomerSearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCustomerDataGrid(CustomerSearchBox.Text);
        }

        public void UpdateCustomerGrid(string CustomerSearch)
        {
            CustomerGridView2.DataSource = null;
            CustomerGridView2.DataSource = crud.PopulateCustomerGrid(CustomerSearch);
        }

        //Item datagrid view on orders screen
        private void ItemSearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateItemDataGrid(ItemSearchBox.Text);
        }

        public void UpdateItemDataGrid(string ItemSearch)
        {
            ItemGridView.DataSource = null;
            ItemGridView.DataSource = crud.SearchOrdersByItem(ItemSearch);
        }

        //Customer datagridview on customer screen
        private void CustomerSearchBox2_TextChanged_1(object sender, EventArgs e)
        {
            UpdateCustomerGrid(CustomerSearchBox2.Text);
        }

        public void UpdateCustomerDataGrid(string CustomerSearch)
        {
            CustomerGridView.DataSource = null;
            CustomerGridView.DataSource = crud.SearchOrdersByCustomer(CustomerSearch);
        }

        //Item datagridview on item screen
        private void ItemSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateItemGrid(ItemSearch.Text);
        }

        public void UpdateItemGrid(string ItemSearch)
        {
            ItemDataGrid.DataSource = null;
            ItemDataGrid.DataSource = crud.PopulateItemGrid(ItemSearch);
        }

        //order datagridview on orders screen
        private void PopulateOrderGrid(int id)
        {
            OrderGridView.DataSource = crud.PopulateOrdersGrid(id);
        }

        //-----------------------------------------------------------------------------------------------------------------//
    }
}
