using Inventory_MS_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels
{
    public class LocationViewModel
    {
        private readonly Location _location;
        public Location Location => _location;

        public string LocationID => _location.LocationID.ToString();
        public string LocationName => _location.LocationName;

        public LocationViewModel(Location location)
        {
            _location = location;
        }
    }
}
