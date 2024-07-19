using Lab02.AverageRainfall;
using Lab02.DistanceTraveled;
using Lab02.ShippingCalculation;
using Lab02.TestScores;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void shippingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ShippingCalculationWindows shippingCalculation = new ShippingCalculationWindows();
            shippingCalculation.Show();
            Close();
        }

        private void distanceRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DistanceTraveledWindow distanceTraveled = new DistanceTraveledWindow();
            distanceTraveled.Show();
            Close();
        }

        private void scoresRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            TestScoresWindow scoresWindow = new TestScoresWindow();
            scoresWindow.Show();
            Close();
        }

        private void rainfallRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AverageRainfallWindow averageRainfall = new AverageRainfallWindow();
            averageRainfall.Show();
            Close();
        }
    }
}