using Lab02.DistanceTraveled;
using Lab02.ShippingCalculation;
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

namespace Lab02.AverageRainfall
{
    /// <summary>
    /// Interaction logic for AverageRainfallWindow.xaml
    /// </summary>
    public partial class AverageRainfallWindow : Window
    {
        double[,] rainfallData;
        int totalYears;
        int yearCount;
        int monthCount;

        public AverageRainfallWindow()
        {
            InitializeComponent();
        }

        private void confirmYearsButton_Click(object sender, RoutedEventArgs e)
        {
            totalYears = int.Parse(yearsTextBox.Text);

            if (totalYears < 2)
            {
                MessageBox.Show("Please more than 1 year");
            }
            else
            {
                rainfallData = new double[totalYears,12];
                dataCollectGrid.IsEnabled = true;
                yearCount = 0;
                monthCount = 0;
                yearTextBlock.Text = "Years : " + (yearCount + 1);
                monthTextBlock.Text = "Month : " + (monthCount + 1);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            rainfallData[yearCount, monthCount] = double.Parse(inchesRainfallTextBox.Text);
            monthCount++;

            if(monthCount > 11)
            {
                yearCount++;
                monthCount = 0;
            }

            if(yearCount >= totalYears)
            {
                List<TableInfoRainfall> tableData = new List<TableInfoRainfall>();
                for (int i = 0; i < totalYears; i++)
                {
                    TableInfoRainfall rowInfo = new TableInfoRainfall(i + 1, rainfallData[i, 0], rainfallData[i, 1], rainfallData[i, 2], rainfallData[i, 3], rainfallData[i, 4], rainfallData[i, 5],
                                                                        rainfallData[i, 6], rainfallData[i, 7], rainfallData[i, 8], rainfallData[i, 9], rainfallData[i, 10], rainfallData[i, 11]);
                    tableData.Add(rowInfo);
                }
                displayDataGrid.ItemsSource = tableData;
                dataCollectGrid.IsEnabled = false;
                yearTextBlock.Text = "Years : 0";
                monthTextBlock.Text = "Month : 0";
            }

            yearTextBlock.Text = "Years : " + (yearCount + 1);
            monthTextBlock.Text = "Month : " + (monthCount + 1);
        }

        private void shippingCalulationApp_Click(object sender, RoutedEventArgs e)
        {
            ShippingCalculationWindows shippingCalculation = new ShippingCalculationWindows();
            shippingCalculation.Show();
            Close();
        }

        private void distanceTraveledApp_Click(object sender, RoutedEventArgs e)
        {
            DistanceTraveledWindow distanceTraveled = new DistanceTraveledWindow();
            distanceTraveled.Show();
            Close();
        }

        private void testScoresApp_Click(object sender, RoutedEventArgs e)
        {
            TestScoresWindow testScoresWindow = new TestScoresWindow();
            testScoresWindow.Show();
            Close();
        }

        private void mainApp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void yearsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            yearsTextBox.Text = "";
        }

        private void inchesRainfallTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            inchesRainfallTextBox.Text = "";
        }
    }
}
