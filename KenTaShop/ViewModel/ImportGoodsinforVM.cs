using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class ImportGoodsinforVM
    {
        public int? IdGoods { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public int? IdProducer { get; set; }

        public double? Vat { get; set; }

        public double? Total { get; set; }
    }
    public class ImportGoodsinforMD : ImportGoodsinforVM
    {
        public int IdImportGoodsinfor { get; set; }
        public virtual Good? IdGoodsNavigation { get; set; }

        public virtual Producer? IdProducerNavigation { get; set; }

        public virtual ICollection<Data.ImportGood> ImportGoods { get; set; } = new List<Data.ImportGood>();
    }
}
