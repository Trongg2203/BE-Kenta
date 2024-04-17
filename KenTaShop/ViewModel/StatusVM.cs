using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class StatusVM
    {
        public int IdStatus { get; set; }

        public string? Statusdetail { get; set; }

        
    }
    public class StatusMD:StatusVM
    {
        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public virtual ICollection<ImportGood> ImportGoods { get; set; } = new List<ImportGood>();
    }
}
