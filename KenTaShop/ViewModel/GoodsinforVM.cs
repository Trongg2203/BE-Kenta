using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class GoodsinforVM
    {

        public int IdGoods { get; set; }

        public string? GoodsDetail { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }

        
    }
    public class GoodsinforMD:GoodsinforVM 
    {
        public virtual Good IdGoodsNavigation { get; set; } = null!;
        public int IdGoodsInfor { get; set; }
    }

    public class DetailGoodinfor
    {
        public string? GoodsDetail { get; set; }

        public string? Size { get; set; }

        public string? Color { get; set; }


    }
}
