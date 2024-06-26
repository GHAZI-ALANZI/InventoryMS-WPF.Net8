﻿using Microsoft.Toolkit.Mvvm.Input;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    public class CreateProductViewModel : ViewModelBase
    {
        private bool _isDisposed = false;


        public string _productName;
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "Name should be at least 4 characters")]
        [MaxLength(100, ErrorMessage = "Name should be at most 100 characters")]
        public string ProductName
        {
            get => _productName;
            set
            {
                SetProperty(ref _productName, value, true);
            }
        }

        public string _productSKU;
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "SKU should be at least 4 characters")]
        [MaxLength(100, ErrorMessage = "SKU should be at most 100 characters")]
        public string ProductSKU
        {
            get => _productSKU;
            set
            {
                SetProperty(ref _productSKU, value, true);
            }
        }


        public string _productUnit;
        [Required(ErrorMessage = "Unit is Required")]
        public string ProductUnit
        {
            get => _productUnit;
            set
            {
                SetProperty(ref _productUnit, value, true);
            }
        }

        public string _productPrice;
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("^[+-]?([0-9]+\\.?[0-9]*|\\.[0-9]+)$", ErrorMessage = "Invalid Input, only decimals are allowed")]
        public string ProductPrice
        {
            get => _productPrice;
            set
            {
                SetProperty(ref _productPrice, value, true);
            }
        }

        public string _productAvailability;
        [Required(ErrorMessage = "Availability is Required")]
        public string ProductAvailability
        {
            get => _productAvailability;
            set
            {
                SetProperty(ref _productAvailability, value, true);
            }
        }

        public string _supplierID;
        [Required(ErrorMessage = "Supplier is Required")]
        public string SupplierID
        {
            get => _supplierID;
            set
            {
                SetProperty(ref _supplierID, value, true);
            }
        }

        public string _categoryID;
        [Required(ErrorMessage = "Category is Required")]
        public string CategoryID
        {
            get => _categoryID;
            set
            {
                SetProperty(ref _categoryID, value, true);
            }
        }


        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly Action _closeDialogCallback;

        private readonly ObservableCollection<CategoryViewModel> _categories;
        public IEnumerable<CategoryViewModel> Categories => _categories;

        private readonly ObservableCollection<LocationViewModel> _locations;
        public IEnumerable<LocationViewModel> Locations => _locations;

        private readonly ObservableCollection<SupplierViewModel> _suppliers;
        public IEnumerable<SupplierViewModel> Suppliers => _suppliers;



        public RelayCommand SubmitCommand { get; }
        public RelayCommand CancelCommand { get; }
        private RelayCommand LoadSuppliersCommand { get; }
        private RelayCommand LoadCategoriesCommand { get; }
        private RelayCommand LoadLocationsCommand { get; }

        public CreateProductViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            _navigationStore = navigationStore;
            _unitOfWork = unitOfWork;
            _closeDialogCallback = closeDialogCallback;

            _suppliers = new ObservableCollection<SupplierViewModel>();
            _locations = new ObservableCollection<LocationViewModel>();
            _categories = new ObservableCollection<CategoryViewModel>();


            SubmitCommand = new RelayCommand(Submit);
            CancelCommand = new RelayCommand(Cancel);

            LoadLocationsCommand = new RelayCommand(LoadLocations);
            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            LoadCategoriesCommand = new RelayCommand(LoadCategories);
        }

        private void Submit()
        {

            ValidateAllProperties();

            if (HasErrors)
            {
                return;
            }

            Product newProduct = new Product()
            {
                ProductID = Guid.NewGuid(),
                ProductName = _productName,
                ProductSKU = _productSKU,
                ProductQuantity = 0,
                ProductUnit = _productUnit,
                ProductPrice = Convert.ToDecimal(_productPrice),
                ProductAvailability = _productAvailability,
                SupplierID = new Guid(_supplierID),
                CategoryID = new Guid(_categoryID)
            };

            _unitOfWork.ProductRepository.Insert(newProduct);
            _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.PRODUCTS, ActionType.CREATE, $"New product created; ProductID: {newProduct.ProductID};"));
            _unitOfWork.Save();

            _closeDialogCallback();
        }


        private void Cancel()
        {
            _closeDialogCallback();
        }

        private void LoadSuppliers()
        {
            _suppliers.Clear();
            foreach (Supplier s in _unitOfWork.SupplierRepository.Get(filter: s => s.SupplierStatus == "Active"))
            {
                _suppliers.Add(new SupplierViewModel(s));
            }

        }

        private void LoadLocations()
        {
            _locations.Clear();
            foreach (Location s in _unitOfWork.LocationRepository.Get())
            {
                _locations.Add(new LocationViewModel(s));
            }

        }

        private void LoadCategories()
        {
            _categories.Clear();
            foreach(Category c in _unitOfWork.CategoryRepository.Get(filter: c => c.CategoryStatus == "Active"))
            {
                _categories.Add(new CategoryViewModel(c));
            }

        }

        public static CreateProductViewModel LoadViewModel(NavigationStore navigationStore, UnitOfWork unitOfWork, Action closeDialogCallback)
        {
            CreateProductViewModel viewModel = new CreateProductViewModel(navigationStore, unitOfWork, closeDialogCallback);
            viewModel.LoadLocationsCommand.Execute(null);
            viewModel.LoadSuppliersCommand.Execute(null);
            viewModel.LoadCategoriesCommand.Execute(null);
            return viewModel;
        }

        



        protected override void Dispose(bool disposing)
        {
            if(!this._isDisposed)
            {
                if(disposing)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
            }
            this._isDisposed = true;

            base.Dispose(disposing);
        }

        public bool CanCreateProduct(object obj)
        {
            return true;
        }
    }
}
