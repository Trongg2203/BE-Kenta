using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Goodsinfor
{
    public int IdGoodsInfor { get; set; }

    public int IdGoods { get; set; }

    public string? GoodsDetail { get; set; }

    public string? Size { get; set; }

    public string? Color { get; set; }

    public virtual Good IdGoodsNavigation { get; set; } = null!;
}
