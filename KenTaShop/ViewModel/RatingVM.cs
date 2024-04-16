using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class RatingVM
    {
        public int IdGoods { get; set; }

        public int? IdBill { get; set; }

        public string? Rating1 { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedDate { get; set; }

       
    }
    public class RatingMD:RatingVM
    {
        public int IdRating { get; set; }
        public virtual Bill? IdBillNavigation { get; set; }

        public virtual Good IdGoodsNavigation { get; set; } = null!;
    }
}
