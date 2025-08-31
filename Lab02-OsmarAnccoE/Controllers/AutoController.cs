using Microsoft.AspNetCore.Mvc;

using Lab02_OsmarAnccoE.Models;
using Lab02_OsmarAnccoE.Dtos.Auto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lab02_OsmarAnccoE.Controllers;

[ApiController]
[Route("OAE/[controller]")]
public class AutoController : ControllerBase
{
    private static List<Auto> _autos = new List<Auto>
    {
        new Auto { id = 1, Marca = "Toyota", Modelo = "Corolla", Placa = "ABC-123" },
        new Auto { id = 2, Marca = "Honda", Modelo = "Civic", Placa = "XYZ-789" }
    };
    
   // GET
        [HttpGet]
        public IActionResult Get()
        {
            var autosDto = _autos.Select(a => new AutoDto
            {
                id = a.id,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Placa = a.Placa
            }).ToList();
            
            return Ok(autosDto);
        }

        // GET
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var auto = _autos.FirstOrDefault(a => a.id == id);
            if (auto == null)
            {
                return NotFound($"Auto con id {id} no encontrado.");
            }

            var autoDto = new AutoDto
            {
                id = auto.id,
                Marca = auto.Marca,
                Modelo = auto.Modelo,
                Placa = auto.Placa
            };

            return Ok(autoDto);
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateAutoDto autoDto)
        {
            var nuevoAuto = new Auto
            {
                Marca = autoDto.Marca,
                Modelo = autoDto.Modelo,
                Placa = autoDto.Placa
            };

            var maxId = _autos.Any() ? _autos.Max(a => a.id) : 0;
            nuevoAuto.id = maxId + 1;
            
            _autos.Add(nuevoAuto);

            var nuevoAutoDto = new AutoDto
            {
                id = nuevoAuto.id,
                Marca = nuevoAuto.Marca,
                Modelo = nuevoAuto.Modelo,
                Placa = nuevoAuto.Placa
            };

            return CreatedAtAction(nameof(GetById), new { id = nuevoAutoDto.id }, nuevoAutoDto);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAutoDto autoDto)
        {
            var autoExistente = _autos.FirstOrDefault(a => a.id == id);
            if (autoExistente == null)
            {
                return NotFound($"Auto con id {id} no encontrado para actualizar.");
            }

            autoExistente.Marca = autoDto.Marca;
            autoExistente.Modelo = autoDto.Modelo;
            autoExistente.Placa = autoDto.Placa;

            return NoContent();
        }
        
        // PATCH
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdateAutoDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("El documento de parcheo no puede ser nulo.");
            }

            var autoExistente = _autos.FirstOrDefault(a => a.id == id);
            if (autoExistente == null)
            {
                return NotFound($"Auto con id {id} no encontrado.");
            }
            
            var autoToPatch = new UpdateAutoDto
            {
                Marca = autoExistente.Marca,
                Modelo = autoExistente.Modelo,
                Placa = autoExistente.Placa
            };

            patchDoc.ApplyTo(autoToPatch);
            
            if (!TryValidateModel(autoToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            autoExistente.Marca = autoToPatch.Marca;
            autoExistente.Modelo = autoToPatch.Modelo;
            autoExistente.Placa = autoToPatch.Placa;

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var autoParaEliminar = _autos.FirstOrDefault(a => a.id == id);
            if (autoParaEliminar == null)
            {
                return NotFound($"Auto con id {id} no encontrado para eliminar.");
            }

            _autos.Remove(autoParaEliminar);
            return NoContent();
        }
}