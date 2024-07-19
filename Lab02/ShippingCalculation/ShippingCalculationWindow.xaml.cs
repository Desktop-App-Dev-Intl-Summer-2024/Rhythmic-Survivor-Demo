using Lab02.AverageRainfall;
using Lab02.DistanceTraveled;
using Lab02.TestScores;
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

namespace Lab02.ShippingCalculation
{
    /// <summary>
    /// Interaction logic for ShippingCalculationWindows.xaml
    /// </summary>
    public partial class ShippingCalculationWindows : Window
    {
        public ShippingCalculationWindows()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            ShippingCharges shipping = new ShippingCharges();
            shipping.weight = double.Parse(packageWeightTextBox.Text);
            shipping.distance = int.Parse(distanceTextBox.Text);
            double cost = shipping.calculateShippingCost();

            if (cost < 0) {
                MessageBox.Show("Please introduce a correct distance");
                costTextBlock.Text = "Shipping Cost:";
            }
            else {
                costTextBlock.Text = "Shipping Cost: $" + cost;
            }
        }

        private void packageWeightTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            packageWeightTextBox.Text = "";
        }

        private void distanceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            distanceTextBox.Text = "";
        }

        private void distanceTraveledApp_Click(object sender, RoutedEventArgs e)
        {
            DistanceTraveledWindow distanceTraveled = new DistanceTraveledWindow();
            distanceTraveled.Show();
            Close();
        }

        private void scoresApp_Click(object sender, RoutedEventArgs e)
        {
            TestScoresWindow scoresWindow = new TestScoresWindow();
            scoresWindow.Show();
            Close();
        }

        private void rainfallAvgApp_Click(object sender, RoutedEventArgs e)
        {
            AverageRainfallWindow averageRainfall = new AverageRainfallWindow();
            averageRainfall.Show();
            Close();
        }

        private void mainApp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
