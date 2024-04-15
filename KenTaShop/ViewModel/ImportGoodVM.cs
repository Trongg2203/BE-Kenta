using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class ImportGoodVM
    {
        public int? IdGoodstype { get; set; }

        public int? IdImportGoodsinfor { get; set; }

        public DateTime? CreatedDate { get; set; }

        public double? ImportTotal { get; set; }

        public int? Idstatus { get; set; }
    }
    public class ImportGoodMD : ImportGoodVM
    {
        public int IdImport { get; set; }
        public virtual Goodstype? IdGoodstypeNavigation { get; set; }

        public virtual ImportGoodsinfor? IdImportGoodsinforNavigation { get; set; }

        public virtual Status? IdstatusNavigation { get; set; }
    }
}
