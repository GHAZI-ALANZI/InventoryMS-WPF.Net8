﻿using Microsoft.Toolkit.Mvvm.Input;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    public class CreateRoleViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        public string _roleName;

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(2, ErrorMessage = "Name should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Name longer than 50 characters is Not Allowed")]
        public string RoleName
        {
            get => _roleName;
            set
            {
                SetProperty(ref _roleName, value, true);
            }

        }


        private string _roleDescription;

        [Required(ErrorMessage = "Description is Required")]
        [MinLength(10, ErrorMessage = "Description should be at least 10 characters long")]
        [MaxLength(100, ErrorMessage = "Description longer than 100 characters is Not Allowed")]
        public string RoleDescription
        {
            get => _roleDescription;
            set
            {
                SetProperty(ref _roleDescription, value, true);
            }
        }


        private string _roleStatus;

        [Required(ErrorMessage = "Status is Required")]
        public string RoleStatus
        {
            get { return _roleStatus; }
            set
            {
                SetProperty(ref _roleStatus, value, true);
            }
        }


        public RolePrivilegesHelper RolePrivilegesHelper { get; }

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly Action _closeDialogCallback;
        
        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateRoleViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            RolePrivilegesHelper = new RolePrivilegesHelper();

            _closeDialogCallback = closeDialogCallback;
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }


        public void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Role newRole = new Role
            {
                RoleID = Guid.NewGuid(),
                RoleName = this.RoleName,
                RoleDescription = this.RoleDescription,
                RoleStatus = this.RoleStatus,

                OrdersView      = RolePrivilegesHelper.OrdersView,
                OrdersAdd       = RolePrivilegesHelper.OrdersAdd,
                OrdersEdit      = RolePrivilegesHelper.OrdersEdit,
                OrdersDelete    = RolePrivilegesHelper.OrdersDelete,


                CustomersView      = RolePrivilegesHelper.CustomersView,
                CustomersAdd       = RolePrivilegesHelper.CustomersAdd,
                CustomersEdit      = RolePrivilegesHelper.CustomersEdit,
                CustomersDelete    = RolePrivilegesHelper.CustomersDelete,


                ProductsView      = RolePrivilegesHelper.ProductsView,
                ProductsAdd       = RolePrivilegesHelper.ProductsAdd,
                ProductsEdit      = RolePrivilegesHelper.ProductsEdit,
                ProductsDelete    = RolePrivilegesHelper.ProductsDelete,


                StoragesView      = RolePrivilegesHelper.StoragesView,
                StoragesAdd       = RolePrivilegesHelper.StoragesAdd,
                StoragesEdit      = RolePrivilegesHelper.StoragesEdit,
                StoragesDelete    = RolePrivilegesHelper.StoragesDelete,


                DefectivesView      = RolePrivilegesHelper.DefectivesView,
                DefectivesAdd       = RolePrivilegesHelper.DefectivesAdd,
                DefectivesEdit      = RolePrivilegesHelper.DefectivesEdit,
                DefectivesDelete    = RolePrivilegesHelper.DefectivesDelete,


                CategoriesView      = RolePrivilegesHelper.CategoriesView,
                CategoriesAdd       = RolePrivilegesHelper.CategoriesAdd,
                CategoriesEdit      = RolePrivilegesHelper.CategoriesEdit,
                CategoriesDelete    = RolePrivilegesHelper.CategoriesDelete,


                LocationsView      = RolePrivilegesHelper.LocationsView,
                LocationsAdd       = RolePrivilegesHelper.LocationsAdd,
                LocationsEdit      = RolePrivilegesHelper.LocationsEdit,
                LocationsDelete    = RolePrivilegesHelper.LocationsDelete,


                SuppliersView      = RolePrivilegesHelper.SuppliersView,
                SuppliersAdd       = RolePrivilegesHelper.SuppliersAdd,
                SuppliersEdit      = RolePrivilegesHelper.SuppliersEdit,
                SuppliersDelete    = RolePrivilegesHelper.SuppliersDelete,


                RolesView      = RolePrivilegesHelper.RolesView,
                RolesAdd       = RolePrivilegesHelper.RolesAdd,
                RolesEdit      = RolePrivilegesHelper.RolesEdit,
                RolesDelete    = RolePrivilegesHelper.RolesDelete,


                StaffsView      = RolePrivilegesHelper.StaffsView,
                StaffsAdd       = RolePrivilegesHelper.StaffsAdd,
                StaffsEdit      = RolePrivilegesHelper.StaffsEdit,
                StaffsDelete    = RolePrivilegesHelper.StaffsDelete,


                LogsView      = RolePrivilegesHelper.LogsView,
                LogsAdd       = RolePrivilegesHelper.LogsAdd,
                LogsEdit      = RolePrivilegesHelper.LogsEdit,
                LogsDelete    = RolePrivilegesHelper.LogsDelete
            };

            _unitOfWork.RoleRepository.Insert(newRole);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.ROLES, ActionType.CREATE, $"New role created; RoleID:{newRole.RoleID};"));
            _unitOfWork.Save();

            _closeDialogCallback();
        }

        public void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateRoleViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            return new CreateRoleViewModel(navigationStore,unitOfWork, closeDialogCallback);
        }

        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    RolePrivilegesHelper.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

    }
}
