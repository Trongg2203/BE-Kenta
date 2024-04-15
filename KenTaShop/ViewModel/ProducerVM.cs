using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class ProducerVM
    {
        public string? Producername { get; set; }

        public string? Location { get; set; }

        public string? Email { get; set; }

        public string? Phonenumber { get; set; }
    }
    public class ProducerMD : ProducerVM
    {
        public int IdProducer { get; set; }
        public virtual ICollection<ImportGoodsinfor> ImportGoodsinfors { get; set; } = new List<ImportGoodsinfor>();
    }
}
