using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class ImportGood
{
    public int IdImport { get; set; }

    public int? IdGoodstype { get; set; }

    public int? IdImportGoodsinfor { get; set; }

    public DateTime? CreatedDate { get; set; }

    public double? ImportTotal { get; set; }

    public int? Idstatus { get; set; }

    public virtual Goodstype? IdGoodstypeNavigation { get; set; }

    public virtual ImportGoodsinfor? IdImportGoodsinforNavigation { get; set; }

    public virtual Status? IdstatusNavigation { get; set; }
}
