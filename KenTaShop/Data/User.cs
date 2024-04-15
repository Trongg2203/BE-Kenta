using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string Pass { get; set; } = null!;

    public bool? Gender { get; set; }

    public string? Email { get; set; }

    public int IdUsertype { get; set; }

    public string? Username { get; set; }

    public int? PhoneNumber { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Userstype IdUsertypeNavigation { get; set; } = null!;
}
