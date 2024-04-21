using Safenet_2._0.Data;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Safenet_2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DataAccess dataAccess = new DataAccess();
            dataAccess.SaveAllFirewallRulesToJSON();
        }
    }

}
