using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class GoodsVM
    {
        public int IdGoods { get; set; }

        public string? GoodsName { get; set; }

        public int? IdGoodstype { get; set; }

        public int? Quantity { get; set; }

        public double? GoodsPrice { get; set; }

       
    }
    public class GoodsMD:GoodsVM
    {
        public virtual ICollection<Billinfor> Billinfors { get; set; } = new List<Billinfor>();

        public virtual ICollection<Goodsinfor> Goodsinfors { get; set; } = new List<Goodsinfor>();

        public virtual Goodstype? IdGoodstypeNavigation { get; set; }

        public virtual ICollection<ImportGoodsinfor> ImportGoodsinfors { get; set; } = new List<ImportGoodsinfor>();

        public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
    public class Goodpic
    {
        public int IdGoods { get; set; }
    }
}
