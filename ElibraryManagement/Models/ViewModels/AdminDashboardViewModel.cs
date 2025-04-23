using ElibraryManagement.Models;
using System;
using System.Collections.Generic;

namespace ElibraryManagement.ViewModels
{
    public class AdminDashboardViewModel
    {
        // Estadísticas generales
        public int TotalLibros { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalInstituciones { get; set; }
        public List<Libro> LibrosRecientes { get; set; } = new();


        // Gráfico de libros por institución
        public List<string> NombresInstituciones { get; set; } = new();
        public List<int> CantidadLibrosPorInstitucion { get; set; } = new();

        // Últimos libros agregados
        public List<LibroReciente> LibrosRecientementeAgregados { get; set; } = new();

        // Últimos usuarios registrados
        public List<UsuarioReciente> UsuariosRecientementeRegistrados { get; set; } = new();
    }

    public class LibroReciente
    {
        public string Titulo { get; set; } = "";
        public string Institucion { get; set; } = "";
        public DateTime FechaAgregado { get; set; }
    }

    public class UsuarioReciente
    {
        public string Nombre { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

