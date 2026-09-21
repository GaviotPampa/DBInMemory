using Microsoft.EntityFrameworkCore;
using NegocioLosDosChinos.Models;

namespace NegocioLosDosChinos.Data
{
    //Clase que representa el contexto de la base de datos
    public class NegocioContext : DbContext
    {
        //Los DbSets representan las tablas de la base de datos
        //Cada DbSet corresponde a una entidad del modelo de datos
        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        //Funcion to configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Negocio");
        }

        //Funcion to configure the relationships between the entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Articulo>()
                .HasOne(a => a.Proveedor) //un articulo tiene un proveedor
                .WithMany(p => p.Articulos) //un proveedor tiene muchos articulos
                .HasForeignKey(a => a.ProveedorID); //la clave foranea es ProveedorID en la tabla Articulo. La relación se establece mediante ProveedorID.

            modelBuilder
                .Entity<Venta>()
                .HasOne(v => v.Articulo) //una venta tiene un articulo
                .WithMany(a => a.Ventas) //un articulo tiene muchas ventas
                .HasForeignKey(v => v.ArticuloID); //la clave foranea es ArticuloID en la tabla Venta. La relación se establece mediante ArticuloID.
        }
    }
}
