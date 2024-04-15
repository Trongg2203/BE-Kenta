using System;
using System.Collections.Generic;

namespace KenTaShop.Data;

public partial class Userstype
{
    public int IdUsertype { get; set; }

    public string? UserDetail { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
