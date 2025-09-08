using Biblioteca.Interfaces;
using Biblioteca.Models;
using System.Text.Json;

namespace Biblioteca.Services;

public class LibroFileService : ILibroService
{
    private readonly string _filePath = "Data/libros.json";
    private List<Libro> _libros;
    private readonly IAutorService _autorService;

    public LibroFileService(IAutorService autorService)
    {
        _autorService = autorService;
        LoadData();
    }

    private void LoadData()
    {
        if (!File.Exists(_filePath))
        {
            _libros = new List<Libro>();
            return;
        }

        var jsonString = File.ReadAllText(_filePath);
        _libros = JsonSerializer.Deserialize<List<Libro>>(jsonString) ?? new List<Libro>();
        
        // Cargar los autores correspondientes
        foreach (var libro in _libros)
        {
            libro.Autor = _autorService.GetById(libro.AutorId);
        }
    }

    private void SaveData()
    {
        var jsonString = JsonSerializer.Serialize(_libros);
        File.WriteAllText(_filePath, jsonString);
    }

    public List<Libro> GetAll()
    {
        return _libros;
    }

    public Libro GetById(int id)
    {
        return _libros.FirstOrDefault(l => l.Id == id);
    }

    public void Add(Libro libro)
    {
        libro.Id = _libros.Count > 0 ? _libros.Max(l => l.Id) + 1 : 1;
        libro.Autor = _autorService.GetById(libro.AutorId);
        _libros.Add(libro);
        SaveData();
    }

    public void Update(Libro libro)
    {
        var index = _libros.FindIndex(l => l.Id == libro.Id);
        if (index != -1)
        {
            libro.Autor = _autorService.GetById(libro.AutorId);
            _libros[index] = libro;
            SaveData();
        }
    }

    public void Delete(int id)
    {
        var libro = _libros.FirstOrDefault(l => l.Id == id);
        if (libro != null)
        {
            _libros.Remove(libro);
            SaveData();
        }
    }

    public List<Libro> SearchByTitle(string titulo)
    {
        return _libros.Where(l => 
            l.Titulo.ToLower().Contains(titulo.ToLower())
        ).ToList();
    }

    public List<Libro> SearchByYear(int año)
    {
        return _libros.Where(l => l.Año == año).ToList();
    }

    public List<Libro> SearchByAuthor(int autorId)
    {
        return _libros.Where(l => l.AutorId == autorId).ToList();
    }

    public List<Libro> SearchByGenre(string genero)
    {
        return _libros.Where(l => 
            l.Genero.ToLower().Contains(genero.ToLower())
        ).ToList();
    }
}
