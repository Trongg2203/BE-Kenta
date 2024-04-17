using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class PictureVM
    {
        public int IdPicture { get; set; }

        public string? Url { get; set; }
        public int IdGoods { get; set; }


    }
    public class PictureMD:PictureVM
    {

        public virtual Good IdGoodsNavigation { get; set; } = null!;
    }
}
