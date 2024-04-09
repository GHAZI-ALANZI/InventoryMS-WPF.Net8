using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Inventory_MS_WPF.ViewModels
{
    public class ViewModelBase : ObservableValidator, IDisposable
    {

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // dispose all unamanage and managed resources
                {
                    // dispose resources here
                }

            }

            // call methods to cleanup the unamanaged resources

            disposed = true;
        }
    }
}
