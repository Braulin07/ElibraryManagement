using System;
using System.Collections.Generic;

namespace ElibraryManagement.Models;

public partial class Institucione
{
    public int IdInstitucion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
