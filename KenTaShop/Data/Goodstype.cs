using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Goodstype
{
    public int IdGoodstype { get; set; }

    public string? GoodstypeDetail { get; set; }

    public double? Displayorder { get; set; }

    public int? FatherFolder { get; set; }

    public int? SonFolder { get; set; }

    public virtual ICollection<Good> Goods { get; set; } = new List<Good>();

    public virtual ICollection<ImportGood> ImportGoods { get; set; } = new List<ImportGood>();
}
