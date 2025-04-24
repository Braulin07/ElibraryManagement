using System;
using System.Collections.Generic;

namespace ElibraryManagement.Models;

public partial class Suscripcione
{
    public int IdSuscripcion { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string FechaSuscripcion { get; set; } = null!;
}
