namespace Biblioteca.Models;

public class Autor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Nacionalidad { get; set; }
    public List<Libro> Libros { get; set; }

    public Autor()
    {
        Libros = new List<Libro>();
    }
}
