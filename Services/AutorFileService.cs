using Biblioteca.Interfaces;
using Biblioteca.Models;
using System.Text.Json;

namespace Biblioteca.Services;

public class AutorFileService : IAutorService
{
    private readonly string _filePath = "Data/autores.json";
    private readonly string _librosFilePath = "Data/libros.json";
    private List<Autor> _autores;

    public AutorFileService()
    {
        LoadData();
        CargarLibros();
    }

    private void LoadData()
    {
        if (!File.Exists(_filePath))
        {
            _autores = new List<Autor>();
            return;
        }

        var jsonString = File.ReadAllText(_filePath);
        _autores = JsonSerializer.Deserialize<List<Autor>>(jsonString) ?? new List<Autor>();
    }

    private void SaveData()
    {
        var jsonString = JsonSerializer.Serialize(_autores);
        File.WriteAllText(_filePath, jsonString);
    }

    private void CargarLibros()
    {
        if (!File.Exists(_librosFilePath))
        {
            return;
        }

        var jsonString = File.ReadAllText(_librosFilePath);
        var libros = JsonSerializer.Deserialize<List<Libro>>(jsonString) ?? new List<Libro>();

        foreach (var autor in _autores)
        {
            autor.Libros = libros.Where(l => l.AutorId == autor.Id).ToList();
        }
    }

    public List<Autor> GetAll()
    {
        return _autores;
    }

    public Autor? GetById(int id)
    {
        var autor = _autores.FirstOrDefault(a => a.Id == id);
        if (autor != null)
        {
            CargarLibros();
        }
        return autor;
    }

    public void Add(Autor autor)
    {
        autor.Id = _autores.Count > 0 ? _autores.Max(a => a.Id) + 1 : 1;
        _autores.Add(autor);
        SaveData();
    }

    public void Update(Autor autor)
    {
        var index = _autores.FindIndex(a => a.Id == autor.Id);
        if (index != -1)
        {
            _autores[index] = autor;
            SaveData();
        }
    }

    public void Delete(int id)
    {
        var autor = _autores.FirstOrDefault(a => a.Id == id);
        if (autor != null)
        {
            _autores.Remove(autor);
            SaveData();
        }
    }

    public List<Autor> SearchByName(string nombre)
    {
        return _autores.Where(a => 
            (a.Nombre + " " + a.Apellido).ToLower().Contains(nombre.ToLower())
        ).ToList();
    }

    public List<Autor> SearchByNationality(string nacionalidad)
    {
        return _autores.Where(a => 
            a.Nacionalidad.ToLower().Contains(nacionalidad.ToLower())
        ).ToList();
    }
}
