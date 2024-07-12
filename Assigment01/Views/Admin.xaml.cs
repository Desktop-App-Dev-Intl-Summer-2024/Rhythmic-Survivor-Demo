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
using Assigment01.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Assigment01
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        HttpClient client = new HttpClient();

        public Admin()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:7231/api/Cart/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void insertButton_Click(object sender, RoutedEventArgs e)
        {
            ItemInfo item = new ItemInfo();
            item.name = nameTextBox.Text;
            item.id = int.Parse(idTextBox.Text);
            item.amount = int.Parse(amountTextBox.Text);
            item.price = double.Parse(priceTextBox.Text);

            var response = await client.PostAsJsonAsync("AddItem", item);
            MessageBox.Show(response.StatusCode.ToString());
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            int searchId = int.Parse(searchIdTextBox.Text);
            var response = await client.GetStringAsync("SearchItem/" + searchId);
            ServerResponse serverResponse = JsonConvert.DeserializeObject<ServerResponse>(response);
            if(serverResponse != null)
            {
                MessageBox.Show(serverResponse.statusCode.ToString() + " " + serverResponse.statusMessage);
                nameTextBox.Text = serverResponse.item.name;
                idTextBox.Text = serverResponse.item.id.ToString();
                amountTextBox.Text = serverResponse.item.amount.ToString();
                priceTextBox.Text = serverResponse.item.price.ToString();

                updateButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Item not found!");
            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            ItemInfo item = new ItemInfo();
            item.name = nameTextBox.Text;
            item.id = int.Parse(idTextBox.Text);
            item.amount = int.Parse(amountTextBox.Text);
            item.price = double.Parse(priceTextBox.Text);

            int searchId = int.Parse(searchIdTextBox.Text);
            var response = await client.PutAsJsonAsync("UpdateItem/" + searchId, item);
            MessageBox.Show(response.StatusCode.ToString());

            nameTextBox.Text = "Name";
            idTextBox.Text = "ID";
            amountTextBox.Text = "Amount";
            priceTextBox.Text = "Price";
            searchIdTextBox.Text = "SearchID";
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int searchId = int.Parse(searchIdTextBox.Text);
            var response = await client.DeleteAsync("DeleteItem/" + searchId);
            MessageBox.Show(response.StatusCode.ToString());

            nameTextBox.Text = "Name";
            idTextBox.Text = "ID";
            amountTextBox.Text = "Amount";
            priceTextBox.Text = "Price";
            searchIdTextBox.Text = "SearchID";
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private async void showButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await client.GetStringAsync("GetAllItems");
            ServerResponse serverResponse = JsonConvert.DeserializeObject<ServerResponse>(response);
            if (serverResponse != null)
            {
                MessageBox.Show(serverResponse.statusCode.ToString() + " " + serverResponse.statusMessage);
                infoProductsTable.ItemsSource = serverResponse.itemsCart;
                DataContext = this;
            }
            else
            {
                MessageBox.Show("Fail getting data!");
            }
        }

        public async Task<List<ItemInfo>> getProductsInfo()
        {
            List<ItemInfo> itemsData = new List<ItemInfo>();
            var response = await client.GetStringAsync("GetAllItems");
            ServerResponse serverResponse = JsonConvert.DeserializeObject<ServerResponse>(response);
            if (serverResponse != null)
            {
                MessageBox.Show(serverResponse.statusCode.ToString() + " " + serverResponse.statusMessage);
                itemsData = serverResponse.itemsCart;
            }
            else
            {
                MessageBox.Show("Fail getting data!");
            }

            return itemsData;
        }

        public async void updateProductsInfo(ItemInfo[] productsSold)
        {
            foreach (ItemInfo item in productsSold)
            {
                int searchId = item.id;
                var response = await client.PutAsJsonAsync("UpdateItem/" + searchId, item);
                Console.WriteLine(response.StatusCode.ToString());
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
