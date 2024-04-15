using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Producer
{
    public int IdProducer { get; set; }

    public string? Producername { get; set; }

    public string? Location { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public virtual ICollection<ImportGoodsinfor> ImportGoodsinfors { get; set; } = new List<ImportGoodsinfor>();
}
