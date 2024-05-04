using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class BillInforVM
    {
        

        public int? Quantity { get; set; }

        public double? Total { get; set; }

        public virtual Good? IdGoodsNavigation { get; set; }

        public virtual Bill? IdbillNavigation { get; set; }
    }

    public class BillInforMD: BillInforVM
    {
        public int? Idbill { get; set; }

        public int? IdGoods { get; set; }
    }
    public class Delete

    {
        public int? Idbill { get; set; }

        public int? IdGoods { get; set; }
    }

    public class AddBillInfor
    {
        public int? Idbill { get; set; }

        public int? IdGoods { get; set; }

        public int? Quantity { get; set; }

        public double? Total { get; set; }
    }
}
