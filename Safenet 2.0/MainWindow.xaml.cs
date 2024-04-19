﻿using Safenet_2._0.Data;
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
        private DataAccess _dal;
        public int SelectedTabIndex { get; set; }


        //int J = 1;
        int J = 1;
        int K = 1;
        // Define the dataset for the datagrid
        private System.Data.DataTable portData;
        private System.Data.DataTable portAppdata;

        public MainWindow()
        {
            
            InitializeComponent();

            _dal = new DataAccess();

            ObservableCollection<Models.Port> rules = _dal.LoadRules();

            FWRulesGrid.ItemsSource = rules;


/*            // Initialize the dataset
            portData = new System.Data.DataTable();
            

            // Add columns to the dataset
            portData.Columns.Add("Port Number", typeof(int));
            portData.Columns.Add("Protocol", typeof(string));
            portData.Columns.Add("Status", typeof(string));
            portData.Columns.Add("Info", typeof(string));

            // Add dummy data to each header in the datatable
            portData.Rows.Add(80, "HTTP", "Open", "Default HTTP port");
            portData.Rows.Add(443, "HTTPS", "Open", "Default HTTPS port");
            portData.Rows.Add(22, "SSH", "Closed", "Secure Shell protocol");
            portData.Rows.Add(21, "FTP", "Closed", "File Transfer Protocol");
            portData.Rows.Add(3389, "RDP", "Closed", "Remote Desktop Protocol");

            // Set the dataset as the ItemsSource for the datagrid
            dataGrid.ItemsSource = portData.DefaultView;*/


            // Initialize the dataset
            portAppdata = new System.Data.DataTable();

            // Add columns to the dataset
            portAppdata.Columns.Add("App name", typeof(string));
            portAppdata.Columns.Add("toggleStatus", typeof(string));
            

            // Add dummy data to each header in the datatable
           portAppdata.Rows.Add("Chrome", "Open");
            portAppdata.Rows.Add("Edge", "Blocked");
            portAppdata.Rows.Add("Discord", "Open");
            portAppdata.Rows.Add("Teams", "Open");

            // Set the dataset as the ItemsSource for the datagrid
            toggledataGrid.ItemsSource = portAppdata.DefaultView;


/*            foreach (DataRow row in portData.Rows)
            {
                
                Button button = new Button();
                button.Name = "button" + J;
                button.Content = "on/off";
                button.Height = 19;
                button.Width = 50;
                button.Click += Button_Click;
                button.Background = Brushes.Beige;
                Buttonpanel.Children.Add(button);

                J++;
                
            }*/

            foreach (DataRow row in portAppdata.Rows)
            {
                
                ToggleButton toggleButton = new ToggleButton();
                toggleButton.Name = "toggleButton" + K;
                toggleButton.Content = "Block/Unblock";
                toggleButton.Height = 19;
                toggleButton.Width = 80;
                toggleButton.Background = Brushes.Beige;
                toggleButton.Click += BlockApp_Click;
                togglebuttonpanel.Children.Add(toggleButton);
                K++;
                
              

            }
        }

        private void Darkmode_switch(object sender, RoutedEventArgs e)
        {

            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.Black;
            this.Background = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button
            Button button = (Button)sender;

            // Get the corresponding row index
           // int rowIndex = Buttonpanel.Children.IndexOf(button);

            // Get the corresponding DataRow
           // DataRow row = portData.Rows[rowIndex];

            // Toggle the status value
   /*         if (row["Status"].ToString() == "Open")
            {
                row["Status"] = "Closed";
            }
            else if (row["Status"].ToString() == "Closed")
            {
                row["Status"] = "Open";
            }*/
        }

        private void SecureIT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All ports have been closed!");
        }

        private void AboutSecureIT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("SecureIT is a program that closes all ports on your computer to prevent hackers from accessing your computer.");
        }
        public void SelectTab(int index)
        {
            MainWindowTabControl.SelectedIndex = index;
        }
        private void EditFirewall_Click_1(object sender, RoutedEventArgs e)
        {
            CustomFirewallRule customFirewallRule = new CustomFirewallRule();
            customFirewallRule.SelectedTabIndex = MainWindowTabControl.SelectedIndex;
            customFirewallRule.Show();
            this.Hide();
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
      
    }

   
}