using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class OrderListViewHelper : ListViewHelperBase<OrderViewModel>
    {
        

        

        public OrderListViewHelper(ObservableCollection<OrderViewModel> databaseCollection, ObservableCollection<OrderViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }

        protected override bool FilterCollection(object obj)
        {
            if(obj is OrderViewModel viewModel)
            {
                return viewModel.Customer.CustomerFullname.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
