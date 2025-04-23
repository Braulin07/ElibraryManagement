using System;
using System.Collections.Generic;

namespace ElibraryManagement.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool IsActive { get; set; }
    public int IdInstitucion { get; set; } // FK

    public virtual Institucione? IdInstitucionNavigation { get; set; }



    public virtual ICollection<Administradore> Administradores { get; set; } = new List<Administradore>();

    public virtual ICollection<RecuperarContrasena> RecuperarContrasenas { get; set; } = new List<RecuperarContrasena>();
}
