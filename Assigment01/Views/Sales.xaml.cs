using System;
using System.Collections.Generic;
using System.Data;
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
using Assigment01.Models;

namespace Assigment01
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        Admin adminApp;
        List<ItemInfo> itemsData;
        List<ItemInfo> shopCart;

        public Sales()
        {
            InitializeComponent();
            setProductsInfo();
        }

        private async void setProductsInfo()
        {
            adminApp = new Admin();
            itemsData = await adminApp.getProductsInfo();
            shopCart = new List<ItemInfo>();

            List<string> names = new List<string>();
            foreach(ItemInfo item in itemsData)
            {
                names.Add(item.name);
            }
            productsComboBox.ItemsSource = names;
            productsComboBox.SelectedIndex = 0;
        }

        private void productsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = productsComboBox.SelectedIndex;
            amountTextBlock.Text = itemsData[index].amount.ToString();
            priceTextBlock.Text = itemsData[index].price.ToString();
        }

        private void addCartButton_Click(object sender, RoutedEventArgs e)
        {
            int amountInserted = int.Parse(amountTextBox.Text);
            int amountAvailable = int.Parse(amountTextBlock.Text);
            int amountLeft = amountAvailable - amountInserted;
            if (amountLeft < 0)
            {
                MessageBox.Show("Please insert a number less than or equal to " + amountAvailable);
                return;
            }

            ItemInfo item = new ItemInfo();
            item.name = itemsData[productsComboBox.SelectedIndex].name;
            item.id = itemsData[productsComboBox.SelectedIndex].id;
            item.amount = amountInserted;
            item.price = double.Parse(priceTextBlock.Text);
            item.amountLeft = amountLeft;
            shopCart.Add(item);

            cartListBox.Items.Clear();
            for(int i = 0; i < shopCart.Count; i++)
            {
                cartListBox.Items.Add(i + 1 + ") " + shopCart[i].ToString());
            }

            amountTextBlock.Text = amountLeft.ToString();
            itemsData[productsComboBox.SelectedIndex].amount = amountLeft;
            amountTextBox.Text = "Kg";
        }

        private void cartListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removeButton.IsEnabled = true;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            ItemInfo itemRemoved = shopCart[cartListBox.SelectedIndex];
            foreach (ItemInfo item in itemsData)
            {
                if (item.id == itemRemoved.id)
                {
                    item.amount += itemRemoved.amount;
                }
            }
            shopCart.Remove(itemRemoved);

            cartListBox.Items.Clear();
            for (int i = 0; i < shopCart.Count; i++)
            {
                cartListBox.Items.Add(i + 1 + ") " + shopCart[i].ToString());
            }

            removeButton.IsEnabled = false;
            int index = productsComboBox.SelectedIndex;
            amountTextBlock.Text = itemsData[index].amount.ToString();
            priceTextBlock.Text = itemsData[index].price.ToString();
        }

        private async void buyItems_Click(object sender, RoutedEventArgs e)
        {
            double total = 0;
            foreach(ItemInfo item in shopCart)
            {
                total += item.getTotalItem();
                item.amount = item.amountLeft;
            }
            MessageBox.Show("Total to pay : " + total);

            adminApp.updateProductsInfo(shopCart.ToArray());
            shopCart.Clear();
            cartListBox.Items.Clear();
            itemsData = await adminApp.getProductsInfo();
        }

        private void adminAppItem_Click(object sender, RoutedEventArgs e)
        {
            adminApp.Show();
            Close();
        }

        private void mainAppItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainApp = new MainWindow();
            mainApp.Show();
            Close();
        }

        private void amountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            amountTextBox.Text = string.Empty;
        }
    }
}
