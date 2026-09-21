namespace NegocioLosDosChinos.Models
{
    public class Articulo
    {
        public int ArticuloID { get; set; }

        public string? Detalle { get; set; }

        public decimal Precio { get; set; }

        public int ProveedorID { get; set; }

        public int Stock { get; set; }

        public int NivelMinimo { get; set; }

        public Proveedor Proveedor { get; set; } = null!;

        public List<Venta> Ventas { get; set; } = new();
    }
}
