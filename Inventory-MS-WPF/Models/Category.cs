using System.ComponentModel.DataAnnotations;

namespace Inventory_MS_WPF.Models
{
    public class Category
    {

        [Key]
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryStatus { get; set; }
        public string CategoryDescription { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
