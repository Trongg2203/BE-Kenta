using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Status
{
    public int IdStatus { get; set; }

    public string? Statusdetail { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<ImportGood> ImportGoods { get; set; } = new List<ImportGood>();
}
