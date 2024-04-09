using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class DefectiveListViewHelper : ListViewHelperBase<DefectiveViewModel>
    {
        

        

        public DefectiveListViewHelper(ObservableCollection<DefectiveViewModel> databaseCollection, ObservableCollection<DefectiveViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is DefectiveViewModel viewModel)
            {
                return viewModel.Product.ProductName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
