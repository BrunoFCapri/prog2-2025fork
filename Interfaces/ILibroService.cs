using Biblioteca.Models;

namespace Biblioteca.Interfaces;

public interface ILibroService
{
    List<Libro> GetAll();
    Libro GetById(int id);
    void Add(Libro libro);
    void Update(Libro libro);
    void Delete(int id);
    List<Libro> SearchByTitle(string titulo);
    List<Libro> SearchByYear(int año);
    List<Libro> SearchByAuthor(int autorId);
    List<Libro> SearchByGenre(string genero);
}
