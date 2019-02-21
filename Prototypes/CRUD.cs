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
                                    MultipleActiveResultSets=True; AttachDbfilename = C:\Users\jamie\source\repos\PROTOS\Prototype1\Prototype1\Database1.mdf;";

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

        public DataTable PopulateCustomerGrid(string forname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateCustomerGrid", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@forname", forname));
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable PopulateItemGrid(string itemname)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("PopulateItemGrid", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                new SqlParameter("@itemname", itemname));
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
