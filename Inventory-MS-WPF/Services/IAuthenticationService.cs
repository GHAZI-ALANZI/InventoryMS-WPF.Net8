using Inventory_MS_WPF.Models;

namespace Inventory_MS_WPF.Services
{
    interface IAuthenticationService
    {
        public Staff Login(string username, string password);
    }
}
