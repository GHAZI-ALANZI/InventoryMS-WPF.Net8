using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class StorageDetailListViewHelper : ListViewHelperBase<ProductLocationViewModel>
    {
        

        

        public StorageDetailListViewHelper(ObservableCollection<ProductLocationViewModel> databaseCollection, ObservableCollection<ProductLocationViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is ProductLocationViewModel viewModel)
            {
                return viewModel.Product.ProductName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
