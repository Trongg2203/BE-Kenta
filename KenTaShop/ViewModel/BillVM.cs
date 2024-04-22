using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class BillVM
    {
        

        public DateTime? BillDate { get; set; }

        public int? IdUser { get; set; }

        public double? BillTotal { get; set; }

        public int? IdStatus { get; set; }

        
    }
    public class BillMD: BillVM
    {
        public int IdBill { get; set; }
        public virtual Status? IdStatusNavigation { get; set; }

        public virtual User? IdUserNavigation { get; set; }
    }

    public class addBill
    {
        public DateTime? BillDate { get; set; }

        public int? IdUser { get; set; }

        public double? BillTotal { get; set; }

        public int? IdStatus { get; set; }
    }
    
    public class deleBill
    {
        public int IdBill { get; set; }
    }

}
