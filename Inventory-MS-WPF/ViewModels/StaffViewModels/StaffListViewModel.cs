﻿using Microsoft.Toolkit.Mvvm.Input;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using Inventory_MS_WPF.ViewModels.ListViewHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    public class StaffListViewModel : ViewModelBase
    {

        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        public StaffListViewHelper StaffListViewHelper { get; }


        private readonly ObservableCollection<StaffViewModel> _staffs;
        public ObservableCollection<StaffViewModel> Staffs { get; }
        
        public RelayCommand CreateStaffCommand { get; }
        public RelayCommand LoadStaffsCommand { get; }
        public RelayCommand<StaffViewModel> RemoveStaffCommand { get; }
        public RelayCommand<StaffViewModel> EditStaffCommand { get; }

        public StaffListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _staffs = new ObservableCollection<StaffViewModel>();
            Staffs = new ObservableCollection<StaffViewModel>();

            StaffListViewHelper = new StaffListViewHelper(_staffs, Staffs);

            LoadStaffsCommand = new RelayCommand(LoadStaffs);
            RemoveStaffCommand = new RelayCommand<StaffViewModel>(RemoveStaff);
            EditStaffCommand = new RelayCommand<StaffViewModel>(EditStaff);
            CreateStaffCommand = new RelayCommand(CreateStaff);

        }


        private void RemoveStaff(StaffViewModel staffViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.StaffRepository.Delete(staffViewModel.Staff);
                _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.STAFFS, ActionType.DELETE, $"Staff deleted; StaffID:{staffViewModel.StaffID};"));
                _unitOfWork.Save();
                _staffs.Remove(staffViewModel);
                StaffListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }
        }


        private void EditStaff(StaffViewModel staffViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditStaffViewModel.LoadViewModel(_navigationStore, _unitOfWork, staffViewModel.Staff, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateStaff()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateStaffViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadStaffs();

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadStaffs()
        {
            _staffs.Clear();
            foreach(Staff u in _unitOfWork.StaffRepository.Get(includeProperties: "Role"))
            {
                _staffs.Add(new StaffViewModel(u));
            }
            StaffListViewHelper.RefreshCollection();
        }

        public static StaffListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            StaffListViewModel viewModel = new StaffListViewModel(navigationStore);
            viewModel.LoadStaffsCommand.Execute(null);

            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            //Note: Implement finalizer only if the object have unmanaged resources

            if (!this._isDisposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                    _unitOfWork.Dispose();
                    _dialogViewModel?.Dispose();
                    StaffListViewHelper.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
