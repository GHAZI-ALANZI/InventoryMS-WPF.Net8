using Inventory_MS_WPF.Models;

namespace Inventory_MS_WPF.Stores
{
    public class AuthenticationStore
    {
        private Staff _currentStaff;
        public Staff CurrentStaff
        {
            get { return _currentStaff; }
            set
            {
                _currentStaff = value;
                OnIsCurrentStaffChanged();
            }
        }


        private bool _isLoggedIn = false;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnIsLoggedInChanged();
            }
        }

        public event Action IsLoggedInChanged;

        private void OnIsLoggedInChanged()
        {
            IsLoggedInChanged?.Invoke();
        }

        public event Action IsCurrentStaffChanged;

        private void OnIsCurrentStaffChanged()
        {
            IsCurrentStaffChanged?.Invoke();
        }


    }
}
