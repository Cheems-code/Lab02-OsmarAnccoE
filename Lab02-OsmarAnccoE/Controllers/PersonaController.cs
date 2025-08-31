using Lab02_OsmarAnccoE.Models;
using Lab02_OsmarAnccoE.Dtos.Persona;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Lab02_OsmarAnccoE.Controllers;


[ApiController]
[Route("OAE/[controller]")]
public class PersonaController : ControllerBase
{
    private static List<Persona> _personas = new List<Persona>
    {
        new Persona { id = 1, nombre = "Juan", DNI = "12345678" },
        new Persona { id = 2, nombre = "Charles", DNI = "876545123" },
        new Persona { id = 3, nombre = "Josh", DNI = "876144734" },
        new Persona { id = 4, nombre = "Luis", DNI = "874125687" },
        new Persona { id = 5, nombre = "Pablo", DNI = "876568519" }
    }; 
    
    // GET: OAE/Persona -> Devuelve una lista de PersonaDto
    [HttpGet]
    public IActionResult Get()
    {
        var personasDto = _personas.Select(p => new PersonaDto
        {
            id = p.id,
            nombre = p.nombre,
            DNI = p.DNI
        }).ToList();
        
        return Ok(personasDto);
    }

    // GET: OAE/Persona/{id} -> Devuelve un solo PersonaDto
    [HttpGet("{id}")]
    public IActionResult GetByid(int id)
    {
        var persona = _personas.FirstOrDefault(p => p.id == id);
        if (persona == null)
        {
            return NotFound($"Persona con id {id} no encontrada.");
        }
        
        var personaDto = new PersonaDto
        {
            id = persona.id,
            nombre = persona.nombre,
            DNI = persona.DNI
        };

        return Ok(personaDto);
    }

    // POST: OAE/Persona -> Recibe un CreatePersonaDto
    [HttpPost]
    public IActionResult Post([FromBody] CreatePersonaDto personaDto)
    {
        if (personaDto == null)
        {
            return BadRequest("Los datos de la persona no pueden ser nulos.");
        }
        
        var nuevaPersona = new Persona
        {
            nombre = personaDto.nombre,
            DNI = personaDto.DNI
        };

        var maxId = _personas.Any() ? _personas.Max(p => p.id) : 0;
        nuevaPersona.id = maxId + 1;
        
        _personas.Add(nuevaPersona);
        
        var nuevaPersonaDto = new PersonaDto
        {
            id = nuevaPersona.id,
            nombre = nuevaPersona.nombre,
            DNI = nuevaPersona.DNI
        };
        
        return CreatedAtAction(nameof(GetByid), new { id = nuevaPersonaDto.id }, nuevaPersonaDto);
    }

    // PUT: OAE/Persona/{id} -> Recibe un UpdatePersonaDto
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdatePersonaDto personaDto)
    {
        var personaExistente = _personas.FirstOrDefault(p => p.id == id);
        if (personaExistente == null)
        {
            return NotFound($"Persona con id {id} no encontrado para actualizar.");
        }
        
        personaExistente.nombre = personaDto.nombre;
        personaExistente.DNI = personaDto.DNI;

        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdatePersonaDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest("El documento de parcheo no puede ser nulo.");
        }

        var personaExistente = _personas.FirstOrDefault(p => p.id == id);
        if (personaExistente == null)
        {
            return NotFound($"Persona con id {id} no encontrada.");
        }

        var personaToPatch = new UpdatePersonaDto
        {
            nombre = personaExistente.nombre,
            DNI = personaExistente.DNI
        };

        patchDoc.ApplyTo(personaToPatch);

        if (!TryValidateModel(personaToPatch))
        {
            return ValidationProblem(ModelState);
        }

        personaExistente.nombre = personaToPatch.nombre;
        personaExistente.DNI = personaToPatch.DNI;

        return NoContent();
    }

    // DELETE: OAE/[controller]/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var personaParaEliminar = _personas.FirstOrDefault(p => p.id == id);

        if (personaParaEliminar == null)
        {
            return NotFound($"Persona con id {id} no encontrado para eliminar.");
        }
        
        _personas.Remove(personaParaEliminar);

        return Ok();
    }
}