﻿using Safenet_2._0.Models;
using NetFwTypeLib; // Windows Firewall
using System.Text.Json; // JSON Sterialization
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Text.Json.Serialization;
using System.Data;

namespace Safenet_2._0.Data
{
    public class DataAccess
    {
        //for relative paths so it fits every user who uses this from github
        private const string RelativePath = "\\source\\repos\\rafezak\\Safenet\\Safenet 2.0\\Data\\";
        private const string FileNameFW = "firewall_rules.json";
        private const string FileNameTips = "SecurityTips.json";

        private string GetFilePathFW()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + RelativePath + FileNameFW;
        }

        private string GetFilePathTips()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + RelativePath + FileNameTips;
        }
        #region Firewall-Ports

        //Load the Firewall Port Rules
        public ObservableCollection<Port> LoadRules()
        {
            try
            {
                string json = File.ReadAllText(GetFilePathFW());
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
            File.WriteAllText(GetFilePathFW(), json);
        }

        public void AddNewRule(ObservableCollection<Port> rules)
        {

            List<Port> newRules;

            if(File.Exists(GetFilePathFW()))
            {
                string jsonString = File.ReadAllText(GetFilePathFW());

                newRules = JsonSerializer.Deserialize<List<Port>>(jsonString);
            }
            else
            {
                newRules = new List<Port>();
            }

            newRules.AddRange(rules);

            string updatedJsongString = JsonSerializer.Serialize(rules);

            File.WriteAllText(GetFilePathFW(), updatedJsongString);

        }

        //get current firewall rules
        public ObservableCollection<Port> GetAllFireWallRules()
        {
            ObservableCollection<Port> rules = new ObservableCollection<Port>();

            try
            {
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

                foreach (INetFwRule rule in firewallPolicy.Rules)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get firewall rules: " + ex.Message);
            }
            return rules;
        }

        public void SaveAllFirewallRulesToJSON()
        {
            ObservableCollection<Port> allRules = GetAllFireWallRules();
            SaveRules(allRules);
        }

        public void CreateFirewallRule(Port port)
        {
            try
            {
                INetFwRule portRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                portRule.Name = port.Name;
                portRule.Description = port.Description;
                portRule.Protocol = port.Protocol;
                portRule.LocalPorts = port.LocalPorts;
                portRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW; // Or use NET_FW_ACTION_.NET_FW_ACTION_BLOCK for blocking
                portRule.Enabled = true;

                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Add(portRule);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create firewall rule: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteFirewallRule(Port port)
        {
            try
            {
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallPolicy.Rules.Remove(port.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete firewall rule: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteFirewallRuleFromJson(Port port)
        {
            try
            {
                ObservableCollection<Port> ports = LoadRules();
                // Find and remove the rule from the collection
                var ruleToRemove = ports.FirstOrDefault(p => p.Name == port.Name);
                if (ruleToRemove != null)
                {
                    ports.Remove(ruleToRemove);
                    SaveRules(ports);
                }
                else
                {
                    MessageBox.Show("Rule not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete firewall rule: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }  
        }
        public void Refresh()
        {
            LoadRules();
        }

        #endregion


        #region read tips from json

        public ObservableCollection<Tip> LoadTips()
        {
            try
            {
                string json = File.ReadAllText(GetFilePathTips());
                return JsonSerializer.Deserialize<ObservableCollection<Tip>>(json);
            }
            catch (FileNotFoundException)
            {
                // If the file is not found, return an empty collection
                return new ObservableCollection<Tip>();
            }
        }


        #endregion

    }
}
