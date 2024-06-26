﻿using Microsoft.Toolkit.Mvvm.Input;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory_MS_WPF.ViewModels
{
    class EditOrderDetailViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private OrderDetail _orderDetail;



        private ProductViewModel _product;

        public ProductViewModel Product
        {
            get { return _product; }
        }


        private string _orderDetailQuantity = "0";

        [Required(ErrorMessage = "Quantity is Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Input")]
        public string OrderDetailQuantity
        {
            get { return _orderDetailQuantity; }
            set
            {
                SetProperty(ref _orderDetailQuantity, value, true);

                int tempQuantity;
                if (int.TryParse(_orderDetailQuantity, out tempQuantity))
                {
                    var newAmount = (_product.Product.ProductPrice * Convert.ToInt32(_orderDetailQuantity)).ToString();
                    SetProperty(ref _orderDetailAmount, newAmount, true, nameof(OrderDetailAmount));
                }
                else
                {
                    SetProperty(ref _orderDetailAmount, "NaN", true, nameof(OrderDetailAmount));
                }

            }
        }

        private string _orderDetailAmount;

        [Required(ErrorMessage = "Amount is Required")]
        [RegularExpression("^[+-]?([0-9]+\\.?[0-9]*|\\.[0-9]+)$", ErrorMessage = "Invalid Input, only decimals are allowed")]
        public string OrderDetailAmount
        {
            get { return _orderDetailAmount; }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private Action _closeDialogCallback;

        public EditOrderDetailViewModel(NavigationStore navigationStore, OrderDetail orderDetail, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();
            _closeDialogCallback = closeDialogCallback;

            SetInitialValues(orderDetail);
            
            
            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SetInitialValues(OrderDetail orderDetail)
        {
            _orderDetail = orderDetail;
            _orderDetailQuantity = orderDetail.OrderDetailQuantity.ToString();
            _orderDetailAmount = orderDetail.OrderDetailAmount.ToString();
            _product = new ProductViewModel(_orderDetail.Product);
        }

        private void Submit()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }
            else if (Convert.ToInt32(_orderDetailQuantity) < 1)
            {
                MessageBox.Show("Only quantities greater than 0 is allowed");
                return;
            }
            else if(Convert.ToInt32(_orderDetailQuantity) >= _orderDetail.OrderDetailQuantity)
            {
                int addedQuantity = Convert.ToInt32(_orderDetailQuantity) - _orderDetail.OrderDetailQuantity;
                if (addedQuantity > _product.Product.ProductQuantity)
                {
                    MessageBox.Show("Not enough stock!");
                    return;
                } else
                {
                    _orderDetail.Product.ProductQuantity -= addedQuantity;
                }
            } else
            {
                _orderDetail.Product.ProductQuantity += _orderDetail.OrderDetailQuantity - Convert.ToInt32(_orderDetailQuantity);
            }

            _orderDetail.OrderDetailQuantity = Convert.ToInt32(_orderDetailQuantity);
            _orderDetail.OrderDetailAmount = Convert.ToDecimal(_orderDetailAmount);

            _closeDialogCallback();
        }

        private void Cancel()
        {
            _closeDialogCallback();
        }


        public static EditOrderDetailViewModel LoadViewModel(NavigationStore navigationStore, OrderDetail orderDetail, Action closeDialogCallback)
        {
            EditOrderDetailViewModel viewModel = new EditOrderDetailViewModel(navigationStore, orderDetail, closeDialogCallback);
            return viewModel;
        }


        protected override void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    _unitOfWork.Dispose();
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }
    }
}
