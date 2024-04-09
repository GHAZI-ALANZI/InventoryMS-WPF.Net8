using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class CustomerListViewHelper : ListViewHelperBase<CustomerViewModel>
    {
        

        

        public CustomerListViewHelper(ObservableCollection<CustomerViewModel> databaseCollection, ObservableCollection<CustomerViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is CustomerViewModel viewModel)
            {
                return viewModel.CustomerFullname.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
