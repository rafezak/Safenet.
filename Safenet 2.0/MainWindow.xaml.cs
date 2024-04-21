using Safenet_2._0.Data;
using Safenet_2._0.Views;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //accessing DAL 
        private DataAccess _dal;

        //making sure that when 2nd window is closed that it goes back to the right tab.
        public int SelectedTabIndex { get; set; }

        // Define the dataset for the datagrid
        private System.Data.DataTable portData;
        private System.Data.DataTable portAppdata;

        public MainWindow()
        {
            InitializeComponent();

            //accessing DAL
            _dal = new DataAccess();

            //loading in the lists
            ObservableCollection<Models.Port> rules = _dal.LoadRules();
            ObservableCollection<Models.Tip> tips = _dal.LoadTips();

            FWRulesGrid.ItemsSource = rules;

            #region grid layout linking
            portData = new System.Data.DataTable();

            Buttongrid.ItemsSource = null; // Clear the ItemsSource       
            Buttongrid.Items.Clear(); // Clear the items collection
            Buttongrid.ItemsSource = portData.DefaultView;
            portAppdata = new System.Data.DataTable();

            portAppdata.Columns.Add("App name", typeof(string));
            portAppdata.Columns.Add("toggleStatus", typeof(string));

            portAppdata.Rows.Add("Chrome", "Open");
            portAppdata.Rows.Add("Edge", "Blocked");
            portAppdata.Rows.Add("Discord", "Open");
            portAppdata.Rows.Add("Teams", "Open");

           
            toggledataGrid.ItemsSource = portAppdata.DefaultView;

            foreach (var row in FWRulesGrid.Items)
            {
                Button button = new Button();
                button.Name = "button";
                button.Content = "on/off";
                button.Height = 18.9680    ;
                button.Width = 40;
                button.Click += Button_Click;
                button.Background = Brushes.Beige;
                Buttonpanel.Children.Add(button);
            }
            
           
            foreach (DataRow row in portAppdata.Rows)
            {
                ToggleButton toggleButton = new ToggleButton();
                toggleButton.Name = "toggleButton";
                toggleButton.Content = "Block/Unblock";
                toggleButton.Height = 19;
                toggleButton.Width = 80;
                toggleButton.Background = Brushes.Beige;
                toggleButton.Click += BlockApp_Click;
                togglebuttonpanel.Children.Add(toggleButton);
            }
        }
        #endregion

        private void Darkmode_switch(object sender, RoutedEventArgs e)
        {

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.Black;
            this.Background = brush;
        }

        ///why do we even still have this button? @raf -> it serves no purpose and has no function || Just confuses the user.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            Button button = (Button)sender;
        }

        private void SecureIT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings applied!");
        }

        private void AboutSecureIT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("SecureIT is a program that closes all ports on your computer to prevent hackers from accessing your computer.");
        }
        

        private void Appblock_Clicked(object sender, RoutedEventArgs e)
        {
            toggledataGrid.Visibility = Visibility.Visible;
            togglebuttonpanel.Visibility = Visibility.Visible;
           
        }
        
        private void BlockApp_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            ToggleButton toggleButton = (ToggleButton)sender;

            // Get the corresponding row index
            int rowIndex = togglebuttonpanel.Children.IndexOf(toggleButton);

            // Get the corresponding DataRow
            DataRow row = portAppdata.Rows[rowIndex];

            // Toggle the status value
            if (row["toggleStatus"].ToString() == "Open")
            {
                row["toggleStatus"] = "Blocked";
            }
            else if (row["toggleStatus"].ToString() == "Blocked")
            {
                row["toggleStatus"] = "Open";
            }

           //MessageBox.Show(rowIndex.ToString());
        }
      
        public void SelectTab(int index)
        {
            MainWindowTabControl.SelectedIndex = index;
        }

        //go to new page to create rule
        private void EditFirewall_Click_1(object sender, RoutedEventArgs e)
        {
            CustomFirewallRule customFirewallRule = new CustomFirewallRule();
            customFirewallRule.SelectedTabIndex = MainWindowTabControl.SelectedIndex;
            customFirewallRule.Show();
            this.Hide();
        }

        private void DeleteFirewallRule_Click(object sender, RoutedEventArgs e)
        {
            // delete function -> Only deleting in the json because.. of safety reasons > incase of real develeopment it would delete a rule in your firewall || function exists in DAL.



        }


    }
}