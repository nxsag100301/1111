using DATN_Ecommerce.Models;

namespace DATN_Ecommerce.ModelsView
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.Price.Value;
    }
}
