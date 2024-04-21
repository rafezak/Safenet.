using Safenet_2._0.Models;
using Safenet_2._0.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace Safenet_2._0.Views
{
    /// <summary>
    /// Interaction logic for CustomFirewallRule.xaml
    /// </summary>
    public partial class CustomFirewallRule : Window
    {
        public int SelectedTabIndex { get; set; }

        private MainViewModel mainViewModel;

        public CustomFirewallRule()
        {
            InitializeComponent();


            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if(mainWindow != null )
            {
                mainWindow.Show();
                mainWindow.SelectTab(SelectedTabIndex);
            }
            
            this.Close(); //  close the current window 
        }

        private void AddRuleButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve input values from text boxes and combo box
            string name = NameTextBox.Text;
            string description = DescriptionTextBox.Text;
            string localPorts = LocalPortsTextBox.Text;

            if (ProtocolComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Tag.ToString(), out int protocol))
                {
                    // Call AddRule method of MainViewModel
                    mainViewModel.AddRule(name, description, localPorts, protocol) ;
                }
                else
                {
                    MessageBox.Show("Invalid protocol selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a protocol.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
