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

namespace Assigment01
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        Admin adminApp;
        DataTable productsTable;
        List<CartInfo> cartList;

        public Sales()
        {
            InitializeComponent();
            setProductsInfo();
        }

        private void setProductsInfo()
        {
            adminApp = new Admin();
            productsTable = adminApp.getProductsInfo();
            cartList = new List<CartInfo>();

            List<string> names = new List<string>();
            foreach(DataRow row in productsTable.Rows)
            {
                names.Add(row["productName"].ToString());
            }
            productsComboBox.ItemsSource = names;
            productsComboBox.SelectedIndex = 0;
        }

        private void productsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            amountTextBlock.Text = productsTable.Rows[productsComboBox.SelectedIndex]["amount"].ToString();
            priceTextBlock.Text = productsTable.Rows[productsComboBox.SelectedIndex]["price"].ToString();
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

            CartInfo cartInfo = new CartInfo();
            cartInfo.name = productsTable.Rows[productsComboBox.SelectedIndex]["productName"].ToString();
            cartInfo.amount = amountInserted;
            cartInfo.price = double.Parse(priceTextBlock.Text);
            cartInfo.amountLeft = amountLeft;
            cartList.Add(cartInfo);

            cartListBox.Items.Clear();
            for(int i = 0; i < cartList.Count; i++)
            {
                cartListBox.Items.Add(i + 1 + ") " + cartList[i].ToString());
            }

            amountTextBlock.Text = amountLeft.ToString();
            productsTable.Rows[productsComboBox.SelectedIndex]["amount"] = amountLeft;
        }

        private void cartListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removeButton.IsEnabled = true;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            CartInfo cartRemoved = cartList[cartListBox.SelectedIndex];
            foreach (DataRow row in productsTable.Rows)
            {
                if (row["productName"] == cartRemoved.name)
                {
                    int left = int.Parse(row["amount"].ToString());
                    int sum = left + cartRemoved.amount;
                    row["amount"] = sum;
                }
            }
            cartList.Remove(cartRemoved);

            cartListBox.Items.Clear();
            for (int i = 0; i < cartList.Count; i++)
            {
                cartListBox.Items.Add(i + 1 + ") " + cartList[i].ToString());
            }

            removeButton.IsEnabled = false;
            amountTextBlock.Text = productsTable.Rows[productsComboBox.SelectedIndex]["amount"].ToString();
            priceTextBlock.Text = productsTable.Rows[productsComboBox.SelectedIndex]["price"].ToString();
        }

        private void buyItems_Click(object sender, RoutedEventArgs e)
        {
            double total = 0;
            foreach(CartInfo item in cartList)
            {
                total += item.getTotalItem();
            }
            MessageBox.Show("Total to pay : " + total);

            adminApp.updateProductsInfo(cartList.ToArray());
            cartList.Clear();
            cartListBox.Items.Clear();
            productsTable = adminApp.getProductsInfo();
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
