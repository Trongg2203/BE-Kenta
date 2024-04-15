using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Billinfor
{
    public int? Idbill { get; set; }

    public int? IdGoods { get; set; }

    public int? Quantity { get; set; }

    public double? Total { get; set; }

    public int IdBillInfor { get; set; }

    public virtual Good? IdGoodsNavigation { get; set; }

    public virtual Bill? IdbillNavigation { get; set; }
}
