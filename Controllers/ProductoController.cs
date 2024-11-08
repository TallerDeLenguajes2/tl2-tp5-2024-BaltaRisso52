using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
    }

    [HttpPost("api/Producto")]
    public IActionResult crearProducto([FromBody] ProductoPostDto productoPostDto)
    {
        ProductoRepository.CrearProducto(productoPostDto);
        return Created();
    }

    [HttpGet("api/Producto")]
    public ActionResult<List<Producto>> getProductos()
    {
        return Ok(ProductoRepository.GetProductos());
    }

    [HttpPut("api/Producto/{id}")]
    public ActionResult<string> ModificarNombreProducto(int id,[FromForm] string nombre){
        Producto? producto = ProductoRepository.GetProducto(id);
        if (producto is not null)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("No permite nombre vacios");
            }
            producto.Descripcion  = nombre;
            ProductoRepository.ModificarProducto(id, producto);
            return Ok("Modificado con exito");
        }
        return NotFound("El producto no fue encontrado");
    }

    [HttpDelete("api/Producto/{id}")]
    public ActionResult DeleteProducto(int id){
        Producto? producto = ProductoRepository.GetProducto(id);
        if (producto is null)
        {
            return NotFound();
        }
        ProductoRepository.EliminarProducto(id);
        return Ok();
    }


}
