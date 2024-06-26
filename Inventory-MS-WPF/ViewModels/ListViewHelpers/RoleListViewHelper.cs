﻿using Inventory_MS_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels.ListViewHelpers
{
    public class RoleListViewHelper : ListViewHelperBase<RoleViewModel>
    {
        

        

        public RoleListViewHelper(ObservableCollection<RoleViewModel> databaseCollection, ObservableCollection<RoleViewModel> displayCollection)
            :base(databaseCollection, displayCollection)
        {

        }



        protected override bool FilterCollection(object obj)
        {
            if(obj is RoleViewModel viewModel)
            {
                return viewModel.RoleName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}
