using System.Data;
using System.Data.SqlClient;

namespace Assigment02.Models
{
    public class DatabaseModel
    {
        public ServerResponse addItem(SqlConnection connection, ItemInfo item)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "insert into dbo.FarmerStorage values (@name, @id, @amount, @price)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue("@id", item.id);
                command.Parameters.AddWithValue("@amount", item.amount);
                command.Parameters.AddWithValue("@price", item.price);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Item added!";
                    response.item = item;
                    response.itemsCart = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail adding Item!";
                    response.item = null;
                    response.itemsCart = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.item = null;
                response.itemsCart = null;
            }

            return response;
        }

        public ServerResponse getAllItems(SqlConnection connection)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "select * from dbo.FarmerStorage";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                List<ItemInfo> cart = new List<ItemInfo>();
                if(table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ItemInfo item = new ItemInfo();
                        item.name = row["productName"].ToString();
                        item.id = (int)row["productID"];
                        item.amount = (int)row["amount"];
                        item.price = double.Parse(row["price"].ToString());
                        cart.Add(item);
                    }
                }

                if(cart.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Items retrieved!";
                    response.item = null;
                    response.itemsCart = cart;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail retrieving items!";
                    response.item = null;
                    response.itemsCart = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.item = null;
                response.itemsCart = null;
            }

            return response;
        }

        public ServerResponse searchItem(SqlConnection connection, int id)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "select * from dbo.FarmerStorage where productID = '" + id +"'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ItemInfo item = new ItemInfo();
                    item.name = reader["productName"].ToString();
                    item.id = (int)reader["productID"];
                    item.amount = (int)reader["amount"];
                    item.price = double.Parse(reader["price"].ToString());

                    response.statusCode = 200;
                    response.statusMessage = "Item found!";
                    response.item = item;
                    response.itemsCart = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Item not found!";
                    response.item = null;
                    response.itemsCart = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.item = null;
                response.itemsCart = null;
            }

            return response;
        }

        public ServerResponse updateItem(SqlConnection connection, int id, ItemInfo item)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "update dbo.FarmerStorage set productName = @name, productId = @id, amount = @amount, price = @price" +
                               " where productID = @searchId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", item.name);
                command.Parameters.AddWithValue("@id", item.id);
                command.Parameters.AddWithValue("@amount", item.amount);
                command.Parameters.AddWithValue("@price", item.price);
                command.Parameters.AddWithValue("@searchId", id);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Item updated!";
                    response.item = item;
                    response.itemsCart = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail updating item!";
                    response.item = null;
                    response.itemsCart = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.item = null;
                response.itemsCart = null;
            }

            return response;
        }

        public ServerResponse deleteItem(SqlConnection connection, int id)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "delete from dbo.FarmerStorage where productID = '" + id +"'";
                SqlCommand command = new SqlCommand(query, connection);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Item deleted!";
                    response.item = null;
                    response.itemsCart = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail deleting item!";
                    response.item = null;
                    response.itemsCart = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.item = null;
                response.itemsCart = null;
            }

            return response;
        }
    }
}