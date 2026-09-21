namespace NegocioLosDosChinos.Models
{
    public class Proveedor
    {
        public int ProveedorID { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Email { get; set; }
        public List<Articulo> Articulos { get; set; } = new(); //inicializacion vacia
    }
}
