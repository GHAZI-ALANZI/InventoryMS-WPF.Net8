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
    public class DefectiveListViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;

        public DefectiveListViewHelper DefectiveListViewHelper { get; }

        private readonly ObservableCollection<DefectiveViewModel> _defectives;
        public ObservableCollection<DefectiveViewModel> Defectives { get; }

        public RelayCommand CreateDefectiveCommand { get; }
        public RelayCommand LoadDefectivesCommand { get; }
        public RelayCommand<DefectiveViewModel> RemoveDefectiveCommand { get; }
        public RelayCommand<DefectiveViewModel> EditDefectiveCommand { get; }

        public DefectiveListViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _defectives = new ObservableCollection<DefectiveViewModel>();
            Defectives = new ObservableCollection<DefectiveViewModel>();
            DefectiveListViewHelper = new DefectiveListViewHelper(_defectives, Defectives);

            LoadDefectivesCommand = new RelayCommand(LoadDefectives);
            RemoveDefectiveCommand = new RelayCommand<DefectiveViewModel>(RemoveDefective);
            EditDefectiveCommand = new RelayCommand<DefectiveViewModel>(EditDefective);
            CreateDefectiveCommand = new RelayCommand(CreateDefective);

        }


        private void RemoveDefective(DefectiveViewModel defectiveViewModel)
        {
            var result = MessageBox.Show("Do you really want to remove this item?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.DefectiveRepository.Delete(defectiveViewModel.Defective);
                _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.DEFECTIVES, ActionType.DELETE, $"Defective deleted; DefectiveID:{defectiveViewModel.DefectiveID};"));
                _unitOfWork.Save();
                _defectives.Remove(defectiveViewModel);
                DefectiveListViewHelper.RefreshCollection();
                MessageBox.Show("Successful");
            }
        }


        private void EditDefective(DefectiveViewModel defectiveViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditDefectiveViewModel.LoadViewModel(_navigationStore, _unitOfWork, defectiveViewModel.Defective, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CreateDefective()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateDefectiveViewModel.LoadViewModel(_navigationStore, _unitOfWork, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            LoadDefectives();

            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void LoadDefectives()
        {
            _defectives.Clear();
            foreach (Defective u in _unitOfWork.DefectiveRepository.Get(includeProperties: "Product"))
            {
                _defectives.Add(new DefectiveViewModel(u));
            }
            DefectiveListViewHelper.RefreshCollection();
        }

        public static DefectiveListViewModel LoadViewModel(NavigationStore navigationStore)
        {
            DefectiveListViewModel viewModel = new DefectiveListViewModel(navigationStore);
            viewModel.LoadDefectivesCommand.Execute(null);

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
                    DefectiveListViewHelper?.Dispose();
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
