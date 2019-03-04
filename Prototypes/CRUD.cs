using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Prototype_Library
{
    public class CRUD
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Integrated Security = True; 
                                    MultipleActiveResultSets=True; AttachDbfilename = C:\Users\jamie\Documents\Visual Studio 2017\Projects\Prototype1\Prototype1\Database1.mdf;";

        public void AddCustomer(Customer c)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO Customer (Title, Forname, Surname, Country, Addressline1, AddressLine2, Town, County, Postcode, TelephoneNum, Email) Values (@Title, @Forname, @Surname, @Country, @Addressline1, @AddressLine2, @Town, @County, @Postcode, @TelephoneNum, @Email)", con);

                cmd.Parameters.AddWithValue("@Title", c.Title);
                cmd.Parameters.AddWithValue("@Forname", c.Forname);
                cmd.Parameters.AddWithValue("@surname", c.Surname);
                cmd.Parameters.AddWithValue("@Country", c.Country);
                cmd.Parameters.AddWithValue("@AddressLine1", c.AddressLine1);
                cmd.Parameters.AddWithValue("@AddressLine2", c.AddressLine2);
                cmd.Parameters.AddWithValue("@Town", c.Town);
                cmd.Parameters.AddWithValue("@County", c.County);
                cmd.Parameters.AddWithValue("@Postcode", c.Postcode);
                cmd.Parameters.AddWithValue("@TelephoneNum", c.TelephoneNum);
                cmd.Parameters.AddWithValue("@Email", c.Email);

                cmd.ExecuteNonQuery();

                con.Close();

            }
        }

        public void AddItem(Item i)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO Item (ItemName, SKU, Barcode, RetailPrice, PurchasePrice, StockLvl, Description) Values (@ItemName, @SKU, @Barcode, @RetailPrice, @PurchasePrice, @StockLvl, @Description)", con);

                cmd.Parameters.AddWithValue("@ItemName", i.ItemName);
                cmd.Parameters.AddWithValue("@SKU", i.SKU);
                cmd.Parameters.AddWithValue("@Barcode", i.Barcode);
                cmd.Parameters.AddWithValue("@RetailPrice", i.RetailPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", i.PurchasePrice);
                cmd.Parameters.AddWithValue("@StockLvl", i.StockLvl);
                cmd.Parameters.AddWithValue("@Description", i.Description);

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AddManufacturer(Manufacturer m)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO Manufacturer (ManuAddress, ManuContact, ManuName) Values (@ManuAddress, @ManuContact, @ManuName)", con);

                cmd.Parameters.AddWithValue("@ManuAddress", m.ManuAddress);
                cmd.Parameters.AddWithValue("@ManuContact", m.ManuContact);
                cmd.Parameters.AddWithValue("@ManuName", m.ManuName);

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AddSupplier(Supplier s)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO Supplier (SuppName, SuppContact, SuppAddress) Values (@SuppName, @SuppContact, @SuppAddress)", con);

                cmd.Parameters.AddWithValue("@SuppName", s.SuppName);
                cmd.Parameters.AddWithValue("@SuppContact", s.SuppContact);
                cmd.Parameters.AddWithValue("@SuppAddress", s.SuppAddress);

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public int AddOrder(Order o)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO [Order] (CustomerId, Date, Status) OUTPUT INSERTED.OrderId Values (@CustomerId, @Date, @Status)", con);

                cmd.Parameters.AddWithValue("@CustomerId", o.CustomerId);
                cmd.Parameters.AddWithValue("@Date", o.Date);
                cmd.Parameters.AddWithValue("@Status", o.Status);

                var id = cmd.ExecuteScalar();
                return (int)id;
            }
        }

        public void AddBasket(Basket b)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("INSERT INTO Basket (OrderId, ItemId, Quantity, RetailPrice) Values (@OrderId, @ItemId, @Quantity, @RetailPrice)", con);

                cmd.Parameters.AddWithValue("@OrderId", b.OrderId);
                cmd.Parameters.AddWithValue("@ItemId", b.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", b.Quantity);
                cmd.Parameters.AddWithValue("@RetailPrice", b.RetailPrice);

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateOrderEntry(int id, int iid, int q, string rp)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("UpdateOrderEntry", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@itemid", iid));
                cmd.Parameters.Add(new SqlParameter("@quantity", q));
                cmd.Parameters.Add(new SqlParameter("@retailprice", rp));
                cmd.ExecuteNonQuery();

            }
        }

        public void UpdateCustomerEntry(int id, string t, string f, string s, string c, string a1, string a2, string to, string cc, string p, string tele, string e)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("UpdateCustomerEntry", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@title", t));
                cmd.Parameters.Add(new SqlParameter("@forname", f));
                cmd.Parameters.Add(new SqlParameter("@surname", s));
                cmd.Parameters.Add(new SqlParameter("@country", c));
                cmd.Parameters.Add(new SqlParameter("@addressline1", a1));
                cmd.Parameters.Add(new SqlParameter("@addressline2", a2));
                cmd.Parameters.Add(new SqlParameter("@town", to));
                cmd.Parameters.Add(new SqlParameter("@county", cc));
                cmd.Parameters.Add(new SqlParameter("@postcode", p));
                cmd.Parameters.Add(new SqlParameter("@telephonenum", tele));
                cmd.Parameters.Add(new SqlParameter("@email", e));
                cmd.ExecuteNonQuery();

            }
        }
        public void UpdateItemEntry(int id, string s, string i, string b, string r, string p, string sl, string d)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("UpdateItemEntry", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@sku", s));
                cmd.Parameters.Add(new SqlParameter("@itemname", i));
                cmd.Parameters.Add(new SqlParameter("@barcode", b));
                cmd.Parameters.Add(new SqlParameter("@retailprice", r));
                cmd.Parameters.Add(new SqlParameter("@purchaseprice", p));
                cmd.Parameters.Add(new SqlParameter("@stocklvl", sl));
                cmd.Parameters.Add(new SqlParameter("@description", d));
                cmd.ExecuteNonQuery();

            }
        }

        public DataTable SearchOrdersByCustomer(string forname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SearchOrdersByCustomer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@forname", forname));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable PopulateCustomerGridByName(string forname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateCustomerGridByName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@forname", forname));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable PopulateCustomerGridById(string id)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateCustomerGridById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@id", id));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable PopulateItemGridByName(string itemname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateItemGridByName", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@itemname", itemname));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable PopulateItemGridBySku(string sku)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateItemGridBySku", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@sku", sku));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable SearchOrdersByItem(string itemname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SearchOrdersByItem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@itemname", itemname));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable PopulateOrdersGrid(int id)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateOrderGridView", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@custidSelected", id));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
