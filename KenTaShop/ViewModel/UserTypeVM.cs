using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class UserTypeVM
    {

        public string? UserDetail { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

    public class UserTypeMD : UserTypeVM
    {
        public int IdUsertype { get; set; }
    }

    public class EditUserTypeMD
    {
        

        public string? UserDetail { get; set; }
    }
}
