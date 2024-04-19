using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Safenet_2._0.Models;

namespace Safenet_2._0.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Port> Rules { get; set; }
    
        public MainViewModel() 
        {
            //LoadRules();

        }

    
    
    }


}
