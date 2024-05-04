using KenTaShop.Data;

namespace KenTaShop.ViewModel
{
    public class UserVM
    {
        public string Pass { get; set; } = null!;
        public int IdUser { get; set; }

        public bool? Gender { get; set; }

        public string? Email { get; set; }

        public int IdUsertype { get; set; }

        public string? Username { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Location { get; set; }

        public DateTime? CreatedDate { get; set; }

      public string NameUserType { get; set; }
    }

    public class UserMD: UserVM
    {
        public int IdUser { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public virtual Userstype IdUsertypeNavigation { get; set; } = null!;
    }
    
    public class InforUser
    {
        public string? Email { get; set; }
        public int IdUsertype { get; set; }

        public string? Username { get; set; }

        public string Pass { get; set; } = null!;
    }
    public class AdminAdd
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }

    public class Login
    {
        public string? Email { get; set; }

        public string Pass { get; set; } = null!;
    }
    public class Register
    {
        public string? Username { get; set; }
        public string Pass { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }

        public string? Location { get; set; }
    }
    public class ChangePass
    {
        public int IdUser { get; set; }
        public string Pass { get; set; }
    }
    
}
