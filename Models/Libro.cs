using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElibraryManagement.Models;

public partial class Libro
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdLibro { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public DateTime? FechaPublicacion { get; set; }

    public int IdInstitucion { get; set; }

    public string? ImagenUrl { get; set; } = null;

    public string? Portada { get; set; }

    public string? UrlArchivo { get; set; }

    public virtual Institucione? IdInstitucionNavigation { get; set; }

}
