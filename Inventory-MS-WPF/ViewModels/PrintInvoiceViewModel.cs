using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Models;
using Inventory_MS_WPF.Stores;
using Inventory_MS_WPF.Utilities;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using static Inventory_MS_WPF.Utilities.Constants;

namespace Inventory_MS_WPF.ViewModels
{
    public class PrintInvoiceViewModel : ViewModelBase
    {
        private bool _isDisposed = false;

        private OrderViewModel _order;
        public OrderViewModel Order => _order;

        private DateTime _currentDateTime = DateTime.Now;
        public DateTime CurrentDateTime => _currentDateTime;

        private readonly UnitOfWork _unitOfWork;
        private readonly NavigationStore _navigationStore;
        private readonly ObservableCollection<OrderDetailViewModel> _orderDetails;
        public IEnumerable<OrderDetailViewModel> OrderDetails => _orderDetails;


        public RelayCommand LoadOrderDetailsCommand { get; }
        public RelayCommand<InvoiceDocumentControl> PrintCommand { get; }

        public PrintInvoiceViewModel(NavigationStore navigationStore, Guid orderID)
        {
            _navigationStore = navigationStore;
            _unitOfWork = new UnitOfWork();

            _order = new OrderViewModel(_unitOfWork.OrderRepository.Get(filter: o => o.OrderID == orderID, includeProperties: "Customer,OrderDetails,OrderDetails.Product").SingleOrDefault());

            _orderDetails = new ObservableCollection<OrderDetailViewModel>();


            LoadOrderDetailsCommand = new RelayCommand(LoadOrderDetails);
            PrintCommand = new RelayCommand<InvoiceDocumentControl>(Print);
        }

        private void Print(InvoiceDocumentControl userControl)
        {
            InvoiceDocumentControl invoiceDocument = (InvoiceDocumentControl)userControl;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(invoiceDocument, "Invoice Printing.");
                _unitOfWork.LogRepository.Insert(LogUtil.CreateLog(LogCategory.ORDERS, ActionType.PRINT_INVOICE, $"Invoice printed; OrderID: {_order.OrderID};"));
                _unitOfWork.Save();
            }
        }



        private void LoadOrderDetails()
        {
            _orderDetails.Clear();
            foreach (OrderDetail s in _order.Order.OrderDetails)
            {
                _orderDetails.Add(new OrderDetailViewModel(s));
            }
        }

        public static PrintInvoiceViewModel LoadViewModel(NavigationStore navigationStore, Guid orderID)
        {
            PrintInvoiceViewModel viewModel = new PrintInvoiceViewModel(navigationStore, orderID);
            viewModel.LoadOrderDetailsCommand.Execute(null);
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
                }

            }

            // call methods to cleanup the unamanaged resources

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
