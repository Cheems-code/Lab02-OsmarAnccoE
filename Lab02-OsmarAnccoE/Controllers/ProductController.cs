using Microsoft.AspNetCore.Mvc;

namespace Lab02_OsmarAnccoE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Todos los productos");
        }

        [HttpGet("{id}")]
        public IActionResult GetByid(int id)
        {
            if (id == 0)
            {
                return NotFound($"Producto no encontrado");
            }
            return Ok($"Producto encontrado: {id}");
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] object product)
        {
            if (product == null)
            {
                return BadRequest("El producgo no puede ser nulo");
            }
            return CreatedAtAction(nameof(GetByid), new { id = 1 }, product);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] object product)
        {
            if (id == 0)
            {
                return NotFound($"Producto con id {id} no encontrado para actualizar.");
            }
            if (product == null)
            {
                return BadRequest("Los datos del producto no pueden ser nulos.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound($"Producto con id {id} no encontrado para eliminar.");
            }
            return NoContent();
        }
    }
}