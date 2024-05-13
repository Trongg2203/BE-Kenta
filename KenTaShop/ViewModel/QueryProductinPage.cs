using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KenTaShop.ViewModel
{
    public class QueryProductinPage
    {
        public int SoTrang { get; set; } = 1; 
        [BindNever]
        public int SoSpinTrang {  get; set; } 
    }
}
