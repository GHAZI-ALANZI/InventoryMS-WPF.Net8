using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private readonly Log _log;
        public Log Log => _log;

        public string LogID => _log.LogID.ToString();
        public string StaffName => _log.Staff.StaffFirstName + " " + _log.Staff.StaffLastName;
        public string LogCategory => _log.LogCategory;
        public string ActionType => _log.ActionType;
        public string LogDetails => _log.LogDetails;
        public string DateTime => _log.DateTime.ToString();

        public LogViewModel(Log log)
        {
            _log = log;
        }
    }
}
