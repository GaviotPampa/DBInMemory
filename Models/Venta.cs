namespace NegocioLosDosChinos.Models
{
    public class Venta
    {
        public int VentaID { get; set; }

        public int ArticuloID { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        public Articulo? Articulo { get; set; }
    }
}
