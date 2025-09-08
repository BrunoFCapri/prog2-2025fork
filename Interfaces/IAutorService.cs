using Biblioteca.Models;

namespace Biblioteca.Interfaces;

public interface IAutorService
{
    List<Autor> GetAll();
    Autor? GetById(int id);
    void Add(Autor autor);
    void Update(Autor autor);
    void Delete(int id);
    List<Autor> SearchByName(string nombre);
    List<Autor> SearchByNationality(string nacionalidad);
}
