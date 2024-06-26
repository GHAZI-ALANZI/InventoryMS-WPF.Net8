﻿using Microsoft.Toolkit.Mvvm.Input;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    public class CreateOrderViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private Order _order;


        private string _customerID;

        [Required(ErrorMessage = "Customer is Required")]
        public string CustomerID
        {
            get => _customerID;
            set
            {
                SetProperty(ref _customerID, value);
                _customer = _customers.Where(c => c.CustomerID == _customerID).SingleOrDefault();
            }
        }

        private CustomerViewModel _customer;
        public CustomerViewModel Customer
        {
            set
            {
                SetProperty(ref _customer, value);
            }
        }


        private string _orderTotal;


        [Required(ErrorMessage = "OrderTotal is Required")]
        public string OrderTotal
        {
            get { return _orderTotal; }
        }

        private ViewModelBase _dialogViewModel;
        public ViewModelBase DialogViewModel => _dialogViewModel;

        private bool _isDialogOpen = false;
        public bool IsDialogOpen => _isDialogOpen;


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;


        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand NavigateToCreateOrderDetailCommand { get; }
        private RelayCommand LoadCustomersCommand { get; }

        public RelayCommand<OrderDetailViewModel> RemoveOrderDetailCommand { get; }
        public RelayCommand<OrderDetailViewModel> EditOrderDetailCommand { get; }
        public RelayCommand CreateOrderDetailCommand { get; }

        public CreateOrderViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _customers = new ObservableCollection<CustomerViewModel>();
            LoadCustomers(_customers);

            _order = new Order
            {
                OrderID = Guid.NewGuid(),
                OrderDetails = new List<OrderDetail>()
            };

            _unitOfWork.OrderRepository.Insert(_order);
            _orderDetails = new ObservableCollection<OrderDetailViewModel>();
            
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);

            RemoveOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(RemoveOrderDetail);
            EditOrderDetailCommand = new RelayCommand<OrderDetailViewModel>(EditOrderDetail);
            CreateOrderDetailCommand = new RelayCommand(CreateOrderDetail);
        }


        private void Submit()
        {
            ValidateAllProperties();

            if(HasErrors)
            {
                return;
            } else if (_order.OrderDetails.Count == 0)
            {
                MessageBox.Show("Product order list cannot be empty");
                return;
            }

            _order.CustomerID = new Guid(CustomerID);
            _order.DeliveryStatus = "Processing";
            _order.OrderTotal = _orderDetails.Sum(od => Convert.ToDecimal(od.OrderDetailAmount));
            _order.OrderDate = DateTime.Now;

            _unitOfWork.OrderRepository.Insert(_order);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.ORDERS, ActionType.CREATE, $"New order created; OrderID: {_order.OrderID};"));
            _unitOfWork.Save();

            MessageBox.Show("Successful");
            CancelCommand.Execute(null);
        }

        private void Cancel()
        {
            _navigationStore.CurrentViewModel = OrderListViewModel.LoadViewModel(_navigationStore);
        }

        private void RemoveOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _order.OrderDetails.Remove(orderDetailViewModel.OrderDetail);
            _orderDetails.Remove(orderDetailViewModel);

            _orderTotal = _orderDetails.Sum(od => od.OrderDetail.OrderDetailAmount).ToString();
            OnPropertyChanged(nameof(OrderTotal));
        }

        private void EditOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = EditOrderDetailViewModel.LoadViewModel(_navigationStore, orderDetailViewModel.OrderDetail, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }

        private void CloseDialogCallback()
        {
            _orderDetails.Clear();
            foreach(OrderDetail od in _order.OrderDetails)
            {
                _orderDetails.Add(new OrderDetailViewModel(od));
            }

            _orderTotal = _orderDetails.Sum(od => od.OrderDetail.OrderDetailAmount).ToString();
            OnPropertyChanged(nameof(OrderTotal));


            _isDialogOpen = false;
            OnPropertyChanged(nameof(IsDialogOpen));


        }

        private void CreateOrderDetail()
        {
            _dialogViewModel?.Dispose();
            _dialogViewModel = CreateOrderDetailViewModel.LoadViewModel(_navigationStore, _order, CloseDialogCallback);
            OnPropertyChanged(nameof(DialogViewModel));

            _isDialogOpen = true;
            OnPropertyChanged(nameof(IsDialogOpen));
        }


        private void LoadCustomers(ObservableCollection<CustomerViewModel> customers)
        {
            customers.Clear();
            foreach(Customer u in _unitOfWork.CustomerRepository.Get())
            {
                customers.Add(new CustomerViewModel(u));
            }
        }


        public static CreateOrderViewModel LoadViewModel(NavigationStore navigationStore)
        {
            CreateOrderViewModel viewModel = new CreateOrderViewModel(navigationStore);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                    _dialogViewModel?.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
