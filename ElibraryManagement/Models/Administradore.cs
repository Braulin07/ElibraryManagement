using System;
using System.Collections.Generic;

namespace ElibraryManagement.Models;

public partial class Administradore
{
    public int IdAdmin { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
