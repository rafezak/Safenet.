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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Safenet_2._0
{
    /// <summary>
    /// Interaction logic for SafeNetApp.xaml
    /// </summary>
    public partial class SafeNetApp : UserControl
    {
        public SafeNetApp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

       
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameBox.Text == "")
            {
                MessageBox.Show("Please enter a username");
            }

            else if (PasswordBox.Password == "")
            {
                MessageBox.Show("Please enter a password");
            }
            else
            {
                Content = new MainWindow();
            }
        }
    }
}
