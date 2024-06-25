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

namespace Assigment01
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

        private void adminAppButton_Click(object sender, RoutedEventArgs e)
        {
            Admin adminApp = new Admin();
            adminApp.Show();
            Close();
        }

        private void salesAppButton_Click(object sender, RoutedEventArgs e)
        {
            Sales salesApp = new Sales();
            salesApp.Show();
            Close();
        }
    }
}