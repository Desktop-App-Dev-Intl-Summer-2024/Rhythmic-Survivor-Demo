using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Assigment01
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        SqlConnection connector;
        SqlCommand command;

        public Admin()
        {
            InitializeComponent();
        }

        private void establishConnectionServer()
        {
            string connection = "Data Source=LAPAZU;Initial Catalog=AppDevDB;Integrated Security=True;Encrypt=False";
            connector = new SqlConnection(connection);
            connector.Open();
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnectionServer();

                string query = "insert into dbo.FarmerStorage values (@name, @id, @amount, @price)";
                command = new SqlCommand(query, connector);
                command.Parameters.AddWithValue("@name", nameTextBox.Text);
                command.Parameters.AddWithValue("@id", int.Parse(idTextBox.Text));
                command.Parameters.AddWithValue("@amount", int.Parse(amountTextBox.Text));
                command.Parameters.AddWithValue("@price", double.Parse(priceTextBox.Text));

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Data inserted");
                }

                connector.Close();
                updateButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnectionServer();

                string query = "select * from dbo.FarmerStorage where productID = @id";
                command = new SqlCommand(query, connector);
                command.Parameters.AddWithValue("@id", int.Parse(searchIdTextBox.Text));
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    nameTextBox.Text = reader["productName"].ToString();
                    idTextBox.Text = reader["productID"].ToString();
                    amountTextBox.Text = reader["amount"].ToString();
                    priceTextBox.Text = reader["price"].ToString();

                    MessageBox.Show("Data found it");
                }
                else
                {
                    MessageBox.Show("Data not found");
                }

                connector.Close();
                updateButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnectionServer();

                string query = "update dbo.FarmerStorage set productName = @name, productId = @id, amount = @amount, price = @price" +
                               " where productID = @id or productName = @name";
                command = new SqlCommand(query, connector);
                command.Parameters.AddWithValue("@name", nameTextBox.Text);
                command.Parameters.AddWithValue("@id", int.Parse(idTextBox.Text));
                command.Parameters.AddWithValue("@amount", int.Parse(amountTextBox.Text));
                command.Parameters.AddWithValue("@price", double.Parse(priceTextBox.Text));

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Data updated");
                }

                connector.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnectionServer();

                string query = "delete from dbo.FarmerStorage where productID = @id";
                command = new SqlCommand(query, connector);
                command.Parameters.AddWithValue("@id", int.Parse(searchIdTextBox.Text));
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Data deleted");
                }

                connector.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                establishConnectionServer();

                string query = "select * from dbo.FarmerStorage";
                command = new SqlCommand(query, connector);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                infoProductsTable.ItemsSource = table.AsDataView();

                connector.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable getProductsInfo()
        {
            DataTable productsInfo = new DataTable();

            try
            {
                establishConnectionServer();

                string query = "select productName, amount, price from dbo.FarmerStorage";
                command = new SqlCommand(query, connector);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(productsInfo);

                connector.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return productsInfo;
        }

        public void updateProductsInfo(CartInfo[] productsSold)
        {
            try
            {
                establishConnectionServer();

                foreach (CartInfo product in productsSold)
                {
                    string query = "update dbo.FarmerStorage set amount = @amount where productName = @name";
                    command = new SqlCommand(query, connector);
                    command.Parameters.AddWithValue("@amount", product.amountLeft);
                    command.Parameters.AddWithValue("@name", product.name);
                    int r = command.ExecuteNonQuery();
                    if (r == 0)
                    {
                        MessageBox.Show("Product didn't update");
                    }
                }

                connector.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salesAppItem_Click(object sender, RoutedEventArgs e)
        {
            Sales salesApp = new Sales();
            salesApp.Show();
            Close();
        }

        private void menuAppItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainApp = new MainWindow();
            mainApp.Show();
            Close();
        }

        private void nameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = string.Empty;
        }

        private void idTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            idTextBox.Text = string.Empty;
        }

        private void amountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            amountTextBox.Text = string.Empty;
        }

        private void priceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            priceTextBox.Text = string.Empty;
        }

        private void searchIdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchIdTextBox.Text = string.Empty;
        }

        
    }
}
