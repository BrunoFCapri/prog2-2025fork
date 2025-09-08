using Microsoft.AspNetCore.Mvc;
using Biblioteca.Interfaces;
using Biblioteca.Models;

namespace Biblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Autor>> GetAll()
    {
        return Ok(_autorService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Autor> GetById(int id)
    {
        var autor = _autorService.GetById(id);
        if (autor == null)
            return NotFound();
        return Ok(autor);
    }

    [HttpPost]
    public ActionResult<Autor> Create(Autor autor)
    {
        _autorService.Add(autor);
        return CreatedAtAction(nameof(GetById), new { id = autor.Id }, autor);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Autor autor)
    {
        if (id != autor.Id)
            return BadRequest();

        _autorService.Update(autor);
        return NoContent();
    }

    [HttpGet("{id}/libros")]
    public ActionResult<IEnumerable<Libro>> GetLibrosByAutor(int id)
    {
        var autor = _autorService.GetById(id);
        if (autor == null)
            return NotFound($"No se encontró el autor con ID {id}");

        return Ok(autor.Libros);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _autorService.Delete(id);
        return NoContent();
    }

    [HttpGet("search/name/{nombre}")]
    public ActionResult<IEnumerable<Autor>> SearchByName(string nombre)
    {
        return Ok(_autorService.SearchByName(nombre));
    }

    [HttpGet("search/nationality/{nacionalidad}")]
    public ActionResult<IEnumerable<Autor>> SearchByNationality(string nacionalidad)
    {
        return Ok(_autorService.SearchByNationality(nacionalidad));
    }
}
