using Lab02.AverageRainfall;
using Lab02.ShippingCalculation;
using Lab02.TestScores;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace Lab02.DistanceTraveled
{
    /// <summary>
    /// Interaction logic for DistanceTraveledWindow.xaml
    /// </summary>
    public partial class DistanceTraveledWindow : Window
    {
        public DistanceTraveledWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            DistanceTraveled distance = new DistanceTraveled();
            distance.speed = int.Parse(speedTextBox.Text);
            distance.time = int.Parse(timeTextBox.Text);

            List<TableInfo> tableInfos = new List<TableInfo>();
            for (int i = 1; i <= distance.time; i++)
            {
                tableInfos.Add(new TableInfo() { Hour = i, Distance = distance.speed * i });
            }
            distanceTable.ItemsSource = tableInfos;
        }

        private void speedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            speedTextBox.Text = "";
        }

        private void timeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            timeTextBox.Text = "";
        }

        private void shippingCalculatorApp_Click(object sender, RoutedEventArgs e)
        {
            ShippingCalculationWindows shippingCalculation = new ShippingCalculationWindows();
            shippingCalculation.Show();
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
