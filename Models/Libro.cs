namespace Biblioteca.Models;

public class Libro
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public int Año { get; set; }
    public required string Genero { get; set; }
    public int AutorId { get; set; }
    public Autor? Autor { get; set; }
}
