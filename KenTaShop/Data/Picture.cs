using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Picture
{
    public int IdPicture { get; set; }

    public string? Url { get; set; }

    public int IdGoods { get; set; }

    public virtual Good IdGoodsNavigation { get; set; } = null!;
}
