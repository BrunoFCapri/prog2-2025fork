using Microsoft.AspNetCore.Mvc;
using Biblioteca.Interfaces;
using Biblioteca.Models;

namespace Biblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibroController : ControllerBase
{
    private readonly ILibroService _libroService;

    public LibroController(ILibroService libroService)
    {
        _libroService = libroService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Libro>> GetAll()
    {
        return Ok(_libroService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Libro> GetById(int id)
    {
        var libro = _libroService.GetById(id);
        if (libro == null)
            return NotFound();
        return Ok(libro);
    }

    [HttpPost]
    public ActionResult<Libro> Create(Libro libro)
    {
        _libroService.Add(libro);
        return CreatedAtAction(nameof(GetById), new { id = libro.Id }, libro);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Libro libro)
    {
        if (id != libro.Id)
            return BadRequest();

        _libroService.Update(libro);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _libroService.Delete(id);
        return NoContent();
    }

    [HttpGet("search/title/{titulo}")]
    public ActionResult<IEnumerable<Libro>> SearchByTitle(string titulo)
    {
        return Ok(_libroService.SearchByTitle(titulo));
    }

    [HttpGet("search/year/{año}")]
    public ActionResult<IEnumerable<Libro>> SearchByYear(int año)
    {
        return Ok(_libroService.SearchByYear(año));
    }

    [HttpGet("search/author/{autorId}")]
    public ActionResult<IEnumerable<Libro>> SearchByAuthor(int autorId)
    {
        return Ok(_libroService.SearchByAuthor(autorId));
    }

    [HttpGet("search/genre/{genero}")]
    public ActionResult<IEnumerable<Libro>> SearchByGenre(string genero)
    {
        return Ok(_libroService.SearchByGenre(genero));
    }
}
