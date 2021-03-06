﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prototype_Library;
using System.Net.Mail;

namespace Prototype1
{
    public partial class RealityGlitch : Form
    {
        CRUD crud = new CRUD();
        Email email = new Email();
        //-------------------------------------------------------//
        //IDs selected from individual data grids//
        //-------------------------------------------------------//
        int orderID;
        int custID;
        int itemID;
        int CustOrderId;
        string password = "password";
        //-------------------------------------------------------//
        int custIdSelected; //ID selected from customer data grid on orders page
        int itemIdSelected; //ID selected from inventory data grid on orders page
        int orderId;
        public RealityGlitch()
        {
            InitializeComponent();
            titlebox.Text = "Mr";   //Sets title combobox default value
            //Default values for combo boxes// 
            itemSearchFilter.SelectedIndex = 0;
            customerSearchFilter.SelectedIndex = 0;
            //-------------------------------------------------------//
            //Sets all datagridviews to display all values on startup//
            //-------------------------------------------------------//
            UpdateItemDataGrid("");
            UpdateItemGrid("");
            UpdateCustomerDataGrid("");
            UpdateCustomerGrid("");
            UpdateOrderList();
            //-------------------------------------------------------//
            orderId = -1; //Clears the orderID on startup
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //MENU/BACK BUTTONS//
        //-----------------------------------------------------------------------------------------------------------------//

        private void Menu_customer_Click(object sender, EventArgs e)
        {
            customer_panel.Visible = true;
        }

        private void Customer_Back_Click(object sender, EventArgs e)
        {
            customer_panel.Visible = false;
        }

        private void Item_Back_Click(object sender, EventArgs e)
        {
            item_panel.Visible = false;
        }

        private void Menu_item_Click(object sender, EventArgs e)
        {
            item_panel.Visible = true;
        }

        private void Manufacturer_Back_Click(object sender, EventArgs e)
        {
            manufacturer_panel.Visible = false;
        }

        private void Menu_manufacturer_Click(object sender, EventArgs e)
        {
            manufacturer_panel.Visible = true;
        }

        private void Supplier_Back_Click(object sender, EventArgs e)
        {
            supplier_panel.Visible = false;
        }

        private void Menu_Supplier_Click(object sender, EventArgs e)
        {
            supplier_panel.Visible = true;
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //SUBMIT BUTTONS//
        //-----------------------------------------------------------------------------------------------------------------//

        private void Item_submit_Click(object sender, EventArgs e)
        {
            Item i = new Item(skubox.Text, itemtitlebox.Text, barcodebox.Text, retailbox.Text, purchasebox.Text, stockbox.Text, descbox.Text, Convert.ToInt32(timeBox.Text));
            crud.AddItem(i);
            UpdateItemDataGrid("");
        }

        private void Customer_submit_Click(object sender, EventArgs e)
        {
            Customer c = new Customer(titlebox.Text, fornamebox.Text, surnamebox.Text, countrybox.Text, address1box.Text, address2box.Text, townbox.Text, countybox.Text, postcodebox.Text, telebox.Text, emailbox.Text);
            crud.AddCustomer(c);
            email.CustomerEmail(emailbox.Text);
            UpdateCustomerGrid("");
        }

        private void Manufacturer_submit_Click(object sender, EventArgs e)
        {
            Manufacturer m = new Manufacturer(manaddressbox.Text, mancontactbox.Text, mannamebox.Text);
            crud.AddManufacturer(m);
        }

        private void Supplier_submit_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier(supnamebox.Text, supcontactbox.Text, supaddressbox.Text);
            crud.AddSupplier(s);
        }

        private void AddOrder_Click(object sender, EventArgs e)
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
                OrderEmailDetails();

            }
            else
            {
                if (itemQuantity.Text == "")
                {
                    MessageBox.Show("ERROR - PLEASE ENTER ITEM QUANTITY");
                }
                else
                {
                    Basket b = new Basket(orderId, itemIdSelected, Convert.ToInt32(itemQuantity.Text), retailprice);
                    crud.AddBasket(b);
                    crud.OrderDetails(orderId);
                    OrderEmailDetails();
                }
            }
            PopulateOrderGrid(custIdSelected);
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //Pulling IDs when selected in the Order datagridview//
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
        //Pulling ID when selected in the Individual datagridview//
        //-----------------------------------------------------------------------------------------------------------------//

        private void OrderIdPull()
        {
            if (OrderGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = OrderGridView.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = OrderGridView.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["OrderId"].Value);

                orderID = Convert.ToInt32(a);
            }
        }

        private void CustomerIdPull()
        {
            if (CustomerGridView2.SelectedCells.Count > 0)
            {
                int selectedrowindex = CustomerGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = CustomerGridView2.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["CustomerId"].Value);

                custID = Convert.ToInt32(a);
            }
        }

        private void ItemIdPull()
        {
            if (ItemDataGrid.SelectedCells.Count > 0)
            {
                int selectedrowindex = ItemDataGrid.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = ItemDataGrid.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["ItemId"].Value);

                itemID = Convert.ToInt32(a);
            }
        }

        private void CustIdOnOrderPull()
        {
            if (OrderGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = OrderGridView.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = OrderGridView.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["CustomerId"].Value);

                CustOrderId = Convert.ToInt32(a);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //Populate Text boxes for database editting//
        //-----------------------------------------------------------------------------------------------------------------//

        private void OrderGridView_SelectionChanged(object sender, EventArgs e)
        {
            OrderIdPull();
        }
        private void ItemDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            ItemIdPull();
            PopulateItemTextboxes();
        }
        private void CustomerGridView2_SelectionChanged(object sender, EventArgs e)
        {
            CustomerIdPull();
            PopulateCustomerTextboxes();
        }
        private void PopulateItemTextboxes()
        {
            if (ItemDataGrid.SelectedCells.Count > 0)
            {
                int selectedrowindex = ItemDataGrid.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = ItemDataGrid.Rows[selectedrowindex];

                int itemid = Convert.ToInt32(selectedRow.Cells["ItemId"].Value);
                skubox.Text = Convert.ToString(selectedRow.Cells["SKU"].Value);
                itemtitlebox.Text = Convert.ToString(selectedRow.Cells["ItemName"].Value);
                barcodebox.Text = Convert.ToString(selectedRow.Cells["Barcode"].Value);
                retailbox.Text = Convert.ToString(selectedRow.Cells["RetailPrice"].Value);
                purchasebox.Text = Convert.ToString(selectedRow.Cells["PurchasePrice"].Value);
                stockbox.Text = Convert.ToString(selectedRow.Cells["StockLvl"].Value);
                descbox.Text = Convert.ToString(selectedRow.Cells["Description"].Value);
                timeBox.Text = Convert.ToString(selectedRow.Cells["Time"].Value);
            }
        }
        private void PopulateCustomerTextboxes()
        {
            if (CustomerGridView2.SelectedCells.Count > 0)
            {
                int selectedrowindex = CustomerGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = CustomerGridView2.Rows[selectedrowindex];

                int customerid = Convert.ToInt32(selectedRow.Cells["CustomerId"].Value);
                titlebox.Text = Convert.ToString(selectedRow.Cells["Title"].Value);
                fornamebox.Text = Convert.ToString(selectedRow.Cells["Forname"].Value);
                surnamebox.Text = Convert.ToString(selectedRow.Cells["Surname"].Value);
                countrybox.Text = Convert.ToString(selectedRow.Cells["Country"].Value);
                address1box.Text = Convert.ToString(selectedRow.Cells["AddressLine1"].Value);
                address2box.Text = Convert.ToString(selectedRow.Cells["AddressLine2"].Value);
                townbox.Text = Convert.ToString(selectedRow.Cells["Town"].Value);
                countybox.Text = Convert.ToString(selectedRow.Cells["County"].Value);
                postcodebox.Text = Convert.ToString(selectedRow.Cells["Postcode"].Value);
                telebox.Text = Convert.ToString(selectedRow.Cells["TelephoneNum"].Value);
                emailbox.Text = Convert.ToString(selectedRow.Cells["Email"].Value);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------//
        //DatagridView update/search//
        //-----------------------------------------------------------------------------------------------------------------//

        //Customer datagridview on orders screen//
        private void CustomerSearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCustomerDataGrid(CustomerSearchBox.Text);
        }
        public void UpdateCustomerDataGrid(string CustomerSearch)
        {
            CustomerGridView.DataSource = null;
            CustomerGridView.DataSource = crud.SearchOrdersByCustomer(CustomerSearch);
        }

        //Item datagrid view on orders screen//
        private void ItemSearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateItemDataGrid(ItemSearchBox.Text);
        }
        public void UpdateItemDataGrid(string ItemSearch)
        {
            ItemGridView.DataSource = null;
            ItemGridView.DataSource = crud.SearchOrdersByItem(ItemSearch);
        }

        //Customer datagridview on customer screen//
        private void CustomerSearchBox2_TextChanged_1(object sender, EventArgs e)
        {
            UpdateCustomerGrid(CustomerSearchBox2.Text);
        }
        public void UpdateCustomerGrid(string CustomerSearch)
        {
            if (customerSearchFilter.SelectedIndex == 0)
            {
                CustomerGridView2.DataSource = null;
                CustomerGridView2.DataSource = crud.PopulateCustomerGridByName(CustomerSearch);
            }
            if (customerSearchFilter.SelectedIndex == 1)
            {
                CustomerGridView2.DataSource = null;
                CustomerGridView2.DataSource = crud.PopulateCustomerGridById(CustomerSearch);
            }

        }

        //Item datagridview on item screen//
        private void ItemSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateItemGrid(ItemSearch.Text);
        }
        public void UpdateItemGrid(string ItemSearch)
        {
            if (itemSearchFilter.SelectedIndex == 0)
            {
                ItemDataGrid.DataSource = null;
                ItemDataGrid.DataSource = crud.PopulateItemGridByName(ItemSearch);
            }
            if (itemSearchFilter.SelectedIndex == 1)
            {
                ItemDataGrid.DataSource = null;
                ItemDataGrid.DataSource = crud.PopulateItemGridBySku(ItemSearch);
            }

        }

        //order datagridview on orders screen//
        private void PopulateOrderGrid(int id)
        {
            OrderGridView.DataSource = crud.PopulateOrdersGrid(id);
        }
        private void OrderSearchBox_TextChanged(object sender, EventArgs e)
        {
            UpdateOrderGrid(OrderSearchBox.Text);
        }
        private void UpdateOrderGrid(string itemname)
        {
            OrderGridView.DataSource = null;
            OrderGridView.DataSource = crud.SearchOrdersByItemName(itemname, custIdSelected);
        }

        private void UpdateOrderList()
        {
            orderListView.DataSource = null;
            orderListView.DataSource = crud.GenerateItemOrder();
        }
        //-----------------------------------------------------------------------------------------------------------------//
        //Editing Database Entries//
        //-----------------------------------------------------------------------------------------------------------------//

        private void UpdateCustomer_Click(object sender, EventArgs e)
        {
            crud.UpdateCustomerEntry(custID, titlebox.Text, fornamebox.Text, surnamebox.Text, countrybox.Text, address1box.Text, address2box.Text, townbox.Text, countybox.Text, postcodebox.Text, telebox.Text, emailbox.Text);
            UpdateCustomerGrid("");
        }

        private void UpdateItem_Click(object sender, EventArgs e)
        {
            crud.UpdateItemEntry(itemID, skubox.Text, itemtitlebox.Text, barcodebox.Text, retailbox.Text, purchasebox.Text, stockbox.Text, descbox.Text);
            UpdateItemGrid("");
        }

        private void UpdateOrder_Click(object sender, EventArgs e)
        {
            int selectedrowindex = ItemGridView.SelectedCells[0].RowIndex;
            DataGridViewRow itemRow = ItemGridView.Rows[selectedrowindex];
            string retailprice = Convert.ToString(itemRow.Cells["RetailPrice"].Value);
            crud.UpdateOrderEntry(orderID, itemIdSelected, Convert.ToInt32(itemQuantity.Text), retailprice);
            PopulateOrderGrid(custIdSelected);
        }
        //-----------------------------------------------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------------------//

        public void OrderEmailDetails()
        {
            DataTable dt = crud.OrderDetails(orderId);
            List<string> orderList = new List<string>();
            foreach (DataRow rows in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    orderList.Add(rows[col].ToString());
                }
            }
            email.OrderEmail(orderList[0], orderList[1], orderList[2], orderList[3], orderList[4], orderList[5], orderList[6], orderList[7]);
        }

        private void passSubmit_Click(object sender, EventArgs e)
        {
            if (passBox.Text == password) //pass = password//
            {
                passwordPanel.Visible = false;
                passBox.Text = "";
            }
            else
            {
                MessageBox.Show("ERROR - INCORRECT PASSWORD");
            }
        }

        private void logoutbutton_Click(object sender, EventArgs e)
        {
            passwordPanel.Visible = true;
        }
    }
}

