using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class ImportGoodsinfor
{
    public int IdImportGoodsinfor { get; set; }

    public int? IdGoods { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public int? IdProducer { get; set; }

    public double? Vat { get; set; }

    public double? Total { get; set; }

    public virtual Good? IdGoodsNavigation { get; set; }

    public virtual Producer? IdProducerNavigation { get; set; }

    public virtual ICollection<ImportGood> ImportGoods { get; set; } = new List<ImportGood>();
}
