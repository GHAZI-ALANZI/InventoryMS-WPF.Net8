﻿using LiveCharts;
using LiveCharts.Wpf;
using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_MS_WPF.ViewModels
{
    class DashboardViewModel : ViewModelBase
    {
        private bool _isDisposed = false;
        private readonly NavigationStore _navigationStore;
        private readonly UnitOfWork _unitOfWork;


        private string _currentMonthRevenue;
        public string CurrentMonthRevenue
        {
            get { return _currentMonthRevenue; }
        }

        private string _currentMonthOrders;
        public string CurrentMonthOrders
        {
            get { return _currentMonthOrders; }
        }

        private string _productsInStock;
        public string ProductsInStock
        {
            get { return _productsInStock; }
        }

        private int _processingOrdersCount;
        public int ProcessingOrdersCount
        {
            get { return _processingOrdersCount; }
        }

        private int _shippedOrdersCount;
        public int ShippedOrdersCount
        {
            get { return _shippedOrdersCount; }
        }

        private int _inTransitOrdersCount;
        public int InTransitOrdersCount
        {
            get { return _inTransitOrdersCount; }
        }

        private int _deliveredOrdersCount;
        public int DeliveredOrdersCount
        {
            get { return _deliveredOrdersCount; }
        }

        private SeriesCollection _monthlySales;
        public SeriesCollection MonthlySales
        {
            get { return _monthlySales; }
        }



        public string [] MonthlySalesXLabel { get; private set; }

        public DashboardViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();


            _currentMonthRevenue = _unitOfWork.OrderRepository.Get(filter: o => o.OrderDate.Month == DateTime.Now.Month).Sum(o => o.OrderTotal).ToString();
            _currentMonthOrders = _unitOfWork.OrderRepository.Get(filter: o => o.OrderDate.Month == DateTime.Now.Month).Count().ToString();
            _productsInStock = _unitOfWork.ProductLocationRepository.Get().Sum(pl => pl.ProductQuantity).ToString();


            _processingOrdersCount = _unitOfWork.OrderRepository.Get(filter: o => o.DeliveryStatus == "Processing").Count();
            _shippedOrdersCount = _unitOfWork.OrderRepository.Get(filter: o => o.DeliveryStatus == "Shipped").Count();
            _inTransitOrdersCount = _unitOfWork.OrderRepository.Get(filter: o => o.DeliveryStatus == "In Transit").Count();
            _deliveredOrdersCount = _unitOfWork.OrderRepository.Get(filter: o => o.DeliveryStatus == "Delivered").Count();

            var monthlySalesData = _unitOfWork.OrderRepository.Get(o => o.OrderDate.Year == DateTime.Now.Year).GroupBy(o => o.OrderDate.Month).OrderBy(o => o.Key).Select(o => new { Month = ((Month)o.Key).ToString(), Sales = o.Sum(a => a.OrderTotal) });

            _monthlySales = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<decimal>(monthlySalesData.Select(d =>  d.Sales))
                }
            };

            MonthlySalesXLabel = monthlySalesData.Select(d => d.Month).ToArray();




        }


        public static DashboardViewModel LoadViewModel(NavigationStore navigationStore)
        {
            DashboardViewModel dashBoardViewModel = new DashboardViewModel(navigationStore);
            return dashBoardViewModel;
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
                }

            }
            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }

    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}
