using Inventory_MS_WPF.Models;

namespace Inventory_MS_WPF.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly Category _category;
        public Category Category => _category;
        public string CategoryID => _category.CategoryID.ToString();
        public string CategoryName => _category.CategoryName;
        public string CategoryDescription => _category.CategoryDescription;
        public string CategoryStatus => _category.CategoryStatus;

        public CategoryViewModel(Category category)
        {
            _category = category;
        }
    }
}
