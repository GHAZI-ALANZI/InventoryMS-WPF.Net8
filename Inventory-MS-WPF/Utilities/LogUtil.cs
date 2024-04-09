using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.ViewModels;
using System.Windows;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.Utilities
{
    public static class LogUtil
    {
        public static Log CreateLog(LogCategory logCategory, ActionType actionType, string details)
        {
            Log newLog = new Log
            {
                LogID = Guid.NewGuid(),
                StaffID = ((MainViewModel)Application.Current.MainWindow.DataContext).AuthenticationStore.CurrentStaff.StaffID,
                LogCategory = logCategory.ToString(),
                ActionType = actionType.ToString(),
                LogDetails = details,
                DateTime = DateTime.Now
            };

            return newLog;
        }
    }
}
