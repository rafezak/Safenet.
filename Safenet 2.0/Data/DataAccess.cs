using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Safenet_2._0.Models;
using NetFwTypeLib; // Windows Firewall
using System.Text.Json; // JSON Sterialization
using System.Collections.ObjectModel;
using System.IO;

namespace Safenet_2._0.Data
{
    public class DataAccess
    {
        private const string FilePathFW = "firewall_rules.json";

        #region Firewall-Ports

        //Load the Firewall Port Rules
        public ObservableCollection<Port> LoadRules()
        {
            try
            {
                string json = File.ReadAllText(FilePathFW);
                return JsonSerializer.Deserialize<ObservableCollection<Port>>(json);
            }
            catch (FileNotFoundException)
            {
                // If the file is not found, return an empty collection
                return new ObservableCollection<Port>();
            }
        }

        //Save current rules
        public void SaveRules(ObservableCollection<Port> rules)
        {
            string json = JsonSerializer.Serialize(rules, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePathFW, json);
        }

        public ObservableCollection<Port> GetAllFireWallRules()
        {
            ObservableCollection<Port> rules = new ObservableCollection<Port>();

            // get existing firewall rules from Windows firewall
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            foreach(INetFwRule rule in firewallPolicy.Rules)
            {
                Port newRule = new Port
                {
                    Name = rule.Name,
                    Description = rule.Description,
                    LocalPorts = rule.LocalPorts,
                    Protocol = rule.Protocol,
                };
                rules.Add(newRule);
            }
            return rules;
        }


        #endregion


        #region Block App 
        #endregion

        #region Tips
        #endregion


        #region Ticket
        #endregion
    }
}
