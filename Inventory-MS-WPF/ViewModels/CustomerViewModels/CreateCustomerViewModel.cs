﻿using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    class CreateCustomerViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        private Guid _staffID = ((MainViewModel)Application.Current.MainWindow.DataContext).AuthenticationStore.CurrentStaff.StaffID;

        private string _customerFirstName;

        [Required(ErrorMessage = "Firstname is Required")]
        [MinLength(2, ErrorMessage = "Firstname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Firstname longer than 50 characters is Not Allowed")]
        public string CustomerFirstName
        {
            get => _customerFirstName;
            set
            {
                SetProperty(ref _customerFirstName, value, true);
            }
        }


        private string _customerLastName;

        [Required(ErrorMessage = "Lastname is Required")]
        [MinLength(2, ErrorMessage = "Lastname should be longer than 2 characters")]
        [MaxLength(50, ErrorMessage = "Lastname longer than 50 characters is Not Allowed")]
        public string CustomerLastName
        {
            get => _customerLastName;
            set
            {
                SetProperty(ref _customerLastName, value, true);
            }
        }

        private string _customerAddress;

        [Required(ErrorMessage = "Address is Required")]
        [MinLength(20, ErrorMessage = "Address should be at least 20 characters long")]
        [MaxLength(300, ErrorMessage = "Address longer than 300 characters is not Allowed")]
        public string CustomerAddress
        {
            get => _customerAddress;
            set
            {
                SetProperty(ref _customerAddress, value, true);
            }
        }

        private string _customerPhone;

        [Required(ErrorMessage = "Phone number is Required")]
        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                SetProperty(ref _customerPhone, value, true);
            }
        }

        private string _customerEmail;

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string CustomerEmail
        {
            get => _customerEmail;
            set
            {
                SetProperty(ref _customerEmail, value, true);
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<StaffViewModel> _staffs;
        public IEnumerable<StaffViewModel> Staffs => _staffs;

        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public CreateCustomerViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _staffs = new ObservableCollection<StaffViewModel>();
            _closeDialogCallback = closeDialogCallback;


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Customer newCustomer = new Customer()
            {
                CustomerID = Guid.NewGuid(),
                StaffID = _staffID,
                CustomerFirstname = _customerFirstName,
                CustomerLastname = _customerLastName,
                CustomerAddress = _customerAddress,
                CustomerPhone = _customerPhone,
                CustomerEmail = _customerEmail,
            };

            _unitOfWork.CustomerRepository.Insert(newCustomer);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.CUSTOMERS, ActionType.CREATE, $"New customer created; CustomerID:{newCustomer.CustomerID};"));
            _unitOfWork.Save();

            _closeDialogCallback();
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static CreateCustomerViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            CreateCustomerViewModel viewModel = new CreateCustomerViewModel(navigationStore, unitOfWork, closeDialogCallback);
            return viewModel;
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
