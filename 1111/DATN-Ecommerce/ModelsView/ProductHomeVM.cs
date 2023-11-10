using DATN_Ecommerce.Models;

namespace DATN_Ecommerce.ModelsView
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
        public int CatId { get; internal set; }
    }
}
