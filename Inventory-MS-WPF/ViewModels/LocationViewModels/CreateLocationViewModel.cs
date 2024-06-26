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
    class CreateLocationViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        public string _locationName;
        [Required(ErrorMessage = "Name is Required")]
        public string LocationName
        {
            get => _locationName;
            set
            {
                SetProperty(ref _locationName, value);
            }

        }

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;


        public event Action<Location> Submitted;

        protected virtual void OnSubmit(Location location)
        {
            Submitted?.Invoke(location);
        }

        public event Action Cancelled;

        protected virtual void OnCancel()
        {
            Cancelled?.Invoke();
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private readonly Action _closeDialogCallback;

        public CreateLocationViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _unitOfWork = unitOfWork;
            _navigationStore = navigationStore;
            _closeDialogCallback = closeDialogCallback;
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }


        public void NavigateToLocationList()
        {
            _navigationStore.CurrentViewModel = LocationListViewModel.LoadViewModel(_navigationStore);
        }

        public void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Location newLocation = new Location()
            {
                LocationID = Guid.NewGuid(),
                LocationName = _locationName
            };

            _unitOfWork.LocationRepository.Insert(newLocation);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.LOCATIONS, ActionType.CREATE, $"New location created; LocationID: {newLocation.LocationID};"));
            _unitOfWork.Save();
            MessageBox.Show("Success");

            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateLocationViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            return new CreateLocationViewModel(navigationStore, unitOfWork, closeDialogCallback);
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
