using System.Runtime.CompilerServices;
using examen_practico_api.Data.Interfaces;
using examen_practico_api.DTO.Producto;
using examen_practico_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace examen_practico_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service) {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> FindAll() {
            List<ProductoModel?> productoso = await _service.FindAll();
            return Ok(productoso);
        }

        [HttpGet("{targetId}")]
        public async Task<IActionResult> FindOne(int targetId) {
            ProductoModel? producto = await _service.FindOne(targetId);
            if (producto == null) return NotFound("No se encontró el producto");
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductoDTO createProductoDTO){
            ProductoModel? producto = await _service.Create(createProductoDTO);

            if (producto == null) return BadRequest("El producto no fué registrado");

            return Ok(producto);
        }

        [HttpPatch("{targetId}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductoDTO updateProductoDTO, int targetId) {
            ProductoModel? producto = await _service.Update(updateProductoDTO, targetId);
            if (producto == null) return NotFound("No se encontró el producto");
            return Ok(producto);
        }

        [HttpDelete("{targetId}")]
        public async Task<IActionResult> Remove(int targetId) {
            ProductoModel? producto = await _service.Remove(targetId);
            if (producto == null) return NotFound("No se encontró el producto");
            return Ok(producto);
        }
    }
}
