using DATN_Ecommerce.Models;

namespace DATN_Ecommerce.ModelsView
{
    public class HomeViewVM
    {
        public List<Product> Products { get; set; }
        public List<TinDang> TinTucs { get; set; }
        public QuangCao quangcao { get; set; }
    }
}
