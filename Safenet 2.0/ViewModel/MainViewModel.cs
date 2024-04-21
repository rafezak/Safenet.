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
using System.ComponentModel;

namespace Safenet_2._0.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DataAccess _dal;
        private ObservableCollection<Tip> filteredTips;


        public ObservableCollection<Port> Rules { get; set; }
        public ObservableCollection<Tip> Tips { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainViewModel()
        {
            _dal = new DataAccess();
            // load port rules from JSON DB
            _dal.LoadRules();
            //load tips from JSON DB
            _dal.LoadTips();

            Rules = _dal.LoadRules();

            if (Rules == null)
            {
                // If Rules collection is null, create a new instance
                Rules = new ObservableCollection<Port>();
            }

            //load tips van JSON DB
            ObservableCollection<Tip> loadedtips = _dal.LoadTips();
            Tips = loadedtips ?? new ObservableCollection<Tip>();

            //Initialize filteredtips

            FilteredTips = new ObservableCollection<Tip>(Tips);

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

            if (Rules == null)
            {
                Rules = new ObservableCollection<Port>();
            
            }
                
            //add new rule to collection
            Rules.Add(newRule);
            
            //Save rules to JSON database
            SaveRules();

            //Create firewall rule
            //_dal.CreateFirewallRule(newRule);

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


        public ObservableCollection<Tip> FilteredTips
        {
            get { return filteredTips; }
            set
            {
                filteredTips = value;
                OnPropertyChanged(nameof(FilteredTips));
            }
        }

        public void FilterTips(string searchText)
        {
            if (Tips == null)
            {
                return; // Do nothing if Tips is null
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                FilteredTips = new ObservableCollection<Tip>(Tips);
            }
            else
            {
                FilteredTips = new ObservableCollection<Tip>(Tips.Where(t =>
                    t.Question.ToLower().Contains(searchText) ||
                    t.Description.ToLower().Contains(searchText) ||
                    t.Subject.ToLower().Contains(searchText)
                ));
            }
        }




    }
}
