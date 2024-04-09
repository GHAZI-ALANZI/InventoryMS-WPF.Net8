using Inventory_MS_WPF.DAL;
using Inventory_MS_WPF.Models;

namespace Inventory_MS_WPF.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private Staff _staff;
        public Staff Staff => _staff;


        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Staff Login(string username, string password)
        {
            Staff storedStaff = _unitOfWork.StaffRepository.Get(s => s.StaffUsername == username && s.StaffPassword == password).SingleOrDefault();

            return storedStaff;
        }

    }

}
