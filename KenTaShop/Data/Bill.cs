using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Bill
{
    public int IdBill { get; set; }

    public DateTime? BillDate { get; set; }

    public int? IdUser { get; set; }

    public double? BillTotal { get; set; }

    public int? IdStatus { get; set; }

    public virtual ICollection<Billinfor> Billinfors { get; set; } = new List<Billinfor>();

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
