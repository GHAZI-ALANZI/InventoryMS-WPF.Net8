using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class StorageListViewHelper : ListViewHelperBase<LocationViewModel>
    {
        

        

        public StorageListViewHelper(ObservableCollection<LocationViewModel> databaseCollection, ObservableCollection<LocationViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is LocationViewModel viewModel)
            {
                return viewModel.LocationName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
