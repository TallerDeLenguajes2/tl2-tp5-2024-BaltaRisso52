using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{
    [HttpPost("api/Presupuesto")]
    public ActionResult CrearPresupuesto([FromForm] string NombreDestinatario)
    {
        Presupuesto presupuesto = new Presupuesto(0, NombreDestinatario, new List<PresupuestoDetalle>());
        PresupuestoRepository.CrearPresupuesto(presupuesto);
        return Created();
    }

    [HttpPost("api/Presupuesto/{id}/ProductoDetalle")]
    public ActionResult<string> AgregarProductoDetalle(int id, [FromForm] int idProducto, [FromForm] int cantidad)
    {
        if (cantidad > 0)
        {
            PresupuestoRepository.AgregarProducto(id, idProducto, cantidad);
            return Ok("Agregado con exito");
        }
        else
        {
            return BadRequest("Cantidad debe ser mayor a 0");
        }
    }

    [HttpGet("api/Presupuesto")]
    public ActionResult<List<Presupuesto>> GetPresupuestos(){
        return Ok(PresupuestoRepository.GetPresupuestos());
    } 

    [HttpDelete("api/Presupuesto/{id}")]
    public ActionResult DeletePresupuesto(int id){
        Presupuesto? presupuesto = PresupuestoRepository.GetPresupuesto(id);
        if (presupuesto is null)
        {
            return NotFound();
        }
        PresupuestoRepository.EliminarPresupuestoPorId(id);
        return Ok();
    }
}