using Lab02.AverageRainfall;
using Lab02.DistanceTraveled;
using Lab02.ShippingCalculation;
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

namespace Lab02.TestScores
{
    /// <summary>
    /// Interaction logic for TestScoresWindow.xaml
    /// </summary>
    public partial class TestScoresWindow : Window
    {
        public TestScoresWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            TestScores scores = new TestScores(int.Parse(score1TextBox.Text), int.Parse(score2TextBox.Text), int.Parse(score3TextBox.Text));
            scoreAverageTextBlock.Text = "Tests Scores Average : " + scores.getTestScoreAverage();
            letterGradeTextBlock.Text = "Letter Grade : " + scores.getLetterGrade();
        }

        private void shippingCalculationApp_Click(object sender, RoutedEventArgs e)
        {
            ShippingCalculationWindows shippingCalculationWindow = new ShippingCalculationWindows();
            shippingCalculationWindow.Show();
            Close();
        }

        private void distanceTraveledApp_Click(object sender, RoutedEventArgs e)
        {
            DistanceTraveledWindow distanceTraveled = new DistanceTraveledWindow();
            distanceTraveled.Show();
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
            MainWindow  main = new MainWindow();
            main.Show();
            Close();
        }

        private void score1TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            score1TextBox.Text = "";
        }

        private void score2TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            score2TextBox.Text = "";
        }

        private void score3TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            score3TextBox.Text = "";
        }
    }
}
