using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Rating
{
    public int IdGoods { get; set; }

    public int? IdBill { get; set; }

    public string? Rating1 { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Bill? IdBillNavigation { get; set; }

    public virtual Good IdGoodsNavigation { get; set; } = null!;
}
