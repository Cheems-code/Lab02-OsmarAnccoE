using Microsoft.AspNetCore.Mvc;

using Lab02_OsmarAnccoE.Models;
using Lab02_OsmarAnccoE.Dtos.Empresa;
using Microsoft.AspNetCore.JsonPatch;

namespace Lab02_OsmarAnccoE.Controllers;

[ApiController]
[Route("OAE/[controller]")]
public class EmpresaController : ControllerBase
{
     private static List<Empresa> _empresas = new List<Empresa>
        {
            new Empresa { id = 1, RazonSocial = "ACME S.A.C.", RUC = "20123456789", Nombre = "ACME Inc.", Direccion = "Av. Siempre Viva 123" },
            new Empresa { id = 2, RazonSocial = "Globex Corporation S.A.", RUC = "20987654321", Nombre = "Globex", Direccion = "Calle Falsa 456" }
        };

        // GET
        [HttpGet]
        public IActionResult Get()
        {
            var empresasDto = _empresas.Select(e => new EmpresaDto
            {
                id = e.id,
                RazonSocial = e.RazonSocial,
                RUC = e.RUC,
                Nombre = e.Nombre,
                Direccion = e.Direccion
            }).ToList();
            
            return Ok(empresasDto);
        }

        // GET 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var empresa = _empresas.FirstOrDefault(e => e.id == id);
            if (empresa == null)
            {
                return NotFound($"Empresa con id {id} no encontrada.");
            }

            var empresaDto = new EmpresaDto
            {
                id = empresa.id,
                RazonSocial = empresa.RazonSocial,
                RUC = empresa.RUC,
                Nombre = empresa.Nombre,
                Direccion = empresa.Direccion
            };

            return Ok(empresaDto);
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateEmpresaDto empresaDto)
        {
            var nuevaEmpresa = new Empresa
            {
                RazonSocial = empresaDto.RazonSocial,
                RUC = empresaDto.RUC,
                Nombre = empresaDto.Nombre,
                Direccion = empresaDto.Direccion
            };

            var maxId = _empresas.Any() ? _empresas.Max(e => e.id) : 0;
            nuevaEmpresa.id = maxId + 1;
            
            _empresas.Add(nuevaEmpresa);

            var nuevaEmpresaDto = new EmpresaDto
            {
                id = nuevaEmpresa.id,
                RazonSocial = nuevaEmpresa.RazonSocial,
                RUC = nuevaEmpresa.RUC,
                Nombre = nuevaEmpresa.Nombre,
                Direccion = nuevaEmpresa.Direccion
            };

            return CreatedAtAction(nameof(GetById), new { id = nuevaEmpresaDto.id }, nuevaEmpresaDto);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateEmpresaDto empresaDto)
        {
            var empresaExistente = _empresas.FirstOrDefault(e => e.id == id);
            if (empresaExistente == null)
            {
                return NotFound($"Empresa con id {id} no encontrada para actualizar.");
            }

            empresaExistente.RazonSocial = empresaDto.RazonSocial;
            empresaExistente.RUC = empresaDto.RUC;
            empresaExistente.Nombre = empresaDto.Nombre;
            empresaExistente.Direccion = empresaDto.Direccion;

            return NoContent();
        }
        
        // PATCH 
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdateEmpresaDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("El documento de parcheo no puede ser nulo.");
            }

            var empresaExistente = _empresas.FirstOrDefault(e => e.id == id);
            if (empresaExistente == null)
            {
                return NotFound($"Empresa con id {id} no encontrada.");
            }

            var empresaToPatch = new UpdateEmpresaDto
            {
                RazonSocial = empresaExistente.RazonSocial,
                RUC = empresaExistente.RUC,
                Nombre = empresaExistente.Nombre,
                Direccion = empresaExistente.Direccion
            };

            patchDoc.ApplyTo(empresaToPatch);

            if (!TryValidateModel(empresaToPatch))
            {
                return ValidationProblem(ModelState);
            }

            empresaExistente.RazonSocial = empresaToPatch.RazonSocial;
            empresaExistente.RUC = empresaToPatch.RUC;
            empresaExistente.Nombre = empresaToPatch.Nombre;
            empresaExistente.Direccion = empresaToPatch.Direccion;

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var empresaParaEliminar = _empresas.FirstOrDefault(e => e.id == id);
            if (empresaParaEliminar == null)
            {
                return NotFound($"Empresa con id {id} no encontrada para eliminar.");
            }

            _empresas.Remove(empresaParaEliminar);
            return NoContent();
        }
}