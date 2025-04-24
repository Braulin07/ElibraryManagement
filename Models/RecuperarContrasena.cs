using System;
using System.Collections.Generic;

namespace ElibraryManagement.Models;

public partial class RecuperarContrasena
{
    public int IdRecuperacion { get; set; }

    public int IdUsuario { get; set; }

    public string CodigoRecuperacion { get; set; } = null!;

    public DateTime FechaSolicitud { get; set; }

    public bool Usado { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
