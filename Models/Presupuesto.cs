public class Presupuesto
{
    private int IdPresupuesto;
    private string nombreDestinatario;
    private List<PresupuestoDetalle> detalles;

    public Presupuesto(int idPresupuesto, string nombreDestinatario, List<PresupuestoDetalle> detalles)
    {
        IdPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.detalles = detalles;
    }

    public Presupuesto(int idPresupuesto, string nombreDestinatario)
    {
        IdPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.detalles = new List<PresupuestoDetalle>();
    }

    public int IdPresupuesto1 { get => IdPresupuesto; set => IdPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestoDetalle> Detalles { get => detalles; set => detalles = value; }

    public int MontoPresupuesto(){
        int monto = 0;
        foreach (var detalle in Detalles)
        {
            monto += detalle.Producto.Precio * detalle.Cantidad;
        }
        return monto;
    }

    public int CantidadProductos(){
        int monto = 0;
        foreach (var detalle in Detalles)
        {
            monto += detalle.Cantidad;
        }
        return monto;
    }

    public double MontoPresupuestoConIVA(){
        int monto = 0;
        foreach (var detalle in Detalles)
        {
            monto += detalle.Producto.Precio * detalle.Cantidad;
        }
        return monto * 1.21;
    }
}