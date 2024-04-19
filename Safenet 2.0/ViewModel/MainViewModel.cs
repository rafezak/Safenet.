using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Safenet_2._0.Models;
using Safenet_2._0.Data;
using System.DirectoryServices;
using NetFwTypeLib;
using System.Windows;

namespace Safenet_2._0.ViewModel
{
    public class MainViewModel
    {
        private DataAccess _dal;
        public ObservableCollection<Port> Rules { get; set; }

        public MainViewModel()
        {
            _dal = new DataAccess();
            // load port rules from JSON DB
            _dal.LoadRules();

        }
        public void AddRule(string name, string description, string localPorts, int protocol)
        {
            var newRule = new Port
            {

                Name = name,
                Description = description,
                LocalPorts = localPorts,
                Protocol = protocol

            };

            Rules.Add(newRule);
            SaveRules();

            //Create firewall rule
            _dal.CreateFirewallRule(newRule);

        }
        //seperating concerns
        private void SaveRules()
        {
            _dal.SaveRules(Rules);
        }

        public void RemoveRule(Port port)
        {
            Rules.Remove(port);
            SaveRules();

            _dal.DeleteFirewallRule(port);
        }



    }
}
