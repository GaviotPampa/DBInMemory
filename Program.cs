using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using NegocioLosDosChinos.Data;
using NegocioLosDosChinos.Models;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new NegocioContext())
        {
            CargarDatos(context);

            Menu(context);
        }
    }

    static void CargarDatos(NegocioContext context)
    {
        // ============================
        // PROVEEDORES
        // ============================

        var proveedor1 = new Proveedor
        {
            Nombre = "Electro Hogar",
            Direccion = "Av. San Martín 123",
            Email = "ventas@electrohogar.com",
        };

        var proveedor2 = new Proveedor
        {
            Nombre = "Distribuidora Central",
            Direccion = "Calle 9 de Julio 456",
            Email = "compras@distribuidoracentral.com",
        };

        var proveedor3 = new Proveedor
        {
            Nombre = "Electrodomésticos del Sur",
            Direccion = "Av. España 789",
            Email = "ventas@electrosur.com",
        };

        context.Proveedores.Add(proveedor1);
        context.Proveedores.Add(proveedor2);
        context.Proveedores.Add(proveedor3);

        context.SaveChanges();

        // ============================
        // ARTÍCULOS
        // ============================

        var articulo1 = new Articulo
        {
            Detalle = "Televisor Smart 50 pulgadas",
            Precio = 500000,
            ProveedorID = proveedor1.ProveedorID,
            Stock = 10,
            NivelMinimo = 3,
        };

        var articulo2 = new Articulo
        {
            Detalle = "Heladera No Frost",
            Precio = 750000.25m,
            ProveedorID = proveedor2.ProveedorID,
            Stock = 2,
            NivelMinimo = 5,
        };

        var articulo3 = new Articulo
        {
            Detalle = "Lavarropas Automático",
            Precio = 620000,
            ProveedorID = proveedor3.ProveedorID,
            Stock = 8,
            NivelMinimo = 3,
        };

        var articulo4 = new Articulo
        {
            Detalle = "Microondas",
            Precio = 180000,
            ProveedorID = proveedor1.ProveedorID,
            Stock = 1,
            NivelMinimo = 4,
        };

        var articulo5 = new Articulo
        {
            Detalle = "Aspiradora",
            Precio = 150000,
            ProveedorID = proveedor3.ProveedorID,
            Stock = 6,
            NivelMinimo = 2,
        };

        context.Articulos.Add(articulo1);
        context.Articulos.Add(articulo2);
        context.Articulos.Add(articulo3);
        context.Articulos.Add(articulo4);
        context.Articulos.Add(articulo5);

        context.SaveChanges();
    }

    static void Menu(NegocioContext context)
    {
        int opcion;

        do
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("       LOS DOS CHINOS");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Realizar venta");
            Console.WriteLine("2. Listar artículos");
            Console.WriteLine("3. Generar listado de reposición");
            Console.WriteLine("0. Salir");
            Console.WriteLine("=================================");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.Ingrese una opción numérica valida.");
                Pausa();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    RealizarVenta(context);
                    break;

                case 2:
                    ListarArticulos(context);
                    break;

                case 3:
                    ListarReposicion(context);
                    break;

                case 0:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.Ingrese una opción valida.");
                    Pausa();
                    break;
            }
        } while (opcion != 0);
    }

    static void RealizarVenta(NegocioContext context)
    {
        Console.Clear();

        Console.WriteLine("=================================");
        Console.WriteLine("          REALIZAR VENTA");
        Console.WriteLine("=================================");

        var articulos = context.Articulos.ToList().OrderBy(a => a.ArticuloID);
        ;

        foreach (var articulo in articulos)
        {
            Console.WriteLine(
                $"ID: {articulo.ArticuloID} | "
                    + $"{articulo.Detalle} | "
                    + $"Precio: ${articulo.Precio} | "
                    + $"Stock: {articulo.Stock}"
            );
        }

        Console.WriteLine();

        Console.Write("Ingrese el ID del artículo: ");

        if (!int.TryParse(Console.ReadLine(), out int articuloID))
        {
            Console.WriteLine("ID inválido.");
            Pausa();
            return;
        }

        var articuloSeleccionado = context.Articulos.FirstOrDefault(a =>
            a.ArticuloID == articuloID
        );

        if (articuloSeleccionado == null)
        {
            Console.WriteLine("El artículo no existe.");
            Pausa();
            return;
        }

        Console.WriteLine($"Artículo seleccionado: {articuloSeleccionado.Detalle}");

        Console.WriteLine($"Stock disponible: {articuloSeleccionado.Stock}");

        Console.Write("Ingrese la cantidad a vender: ");

        if (!int.TryParse(Console.ReadLine(), out int cantidad))
        {
            Console.WriteLine("Cantidad inválida.");
            Pausa();
            return;
        }

        if (cantidad < 1 || cantidad > articuloSeleccionado.Stock)
        {
            Console.WriteLine("La cantidad debe estar entre 1 y el stock disponible.");

            Pausa();
            return;
        }

        decimal monto = articuloSeleccionado.Precio * cantidad;

        var venta = new Venta
        {
            ArticuloID = articuloSeleccionado.ArticuloID,
            Cantidad = cantidad,
            Monto = monto,
        };

        context.Ventas.Add(venta);

        articuloSeleccionado.Stock -= cantidad;

        context.SaveChanges();

        Console.WriteLine();
        Console.WriteLine("VENTA REALIZADA CORRECTAMENTE");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Artículo: {articuloSeleccionado.Detalle}");
        Console.WriteLine($"Cantidad: {cantidad}");
        Console.WriteLine($"Monto total: ${monto}");
        Console.WriteLine($"Stock restante: {articuloSeleccionado.Stock}");

        Pausa();
    }

    static void ListarArticulos(NegocioContext context)
    {
        Console.Clear();
        // ============================
        // MOSTRAR ARTÍCULOS
        // ============================


        Console.WriteLine("=================================");
        Console.WriteLine("       LISTADO DE ARTÍCULOS");
        Console.WriteLine("=================================");

        foreach (var articulo in context.Articulos.OrderBy(a => a.ArticuloID))
        {
            Console.WriteLine(
                $"ID: {articulo.ArticuloID} | "
                    + $"Detalle: {articulo.Detalle} | "
                    + $"Precio: ${articulo.Precio} | "
                    + $"Stock: {articulo.Stock}"
            );
        }

        Pausa();
    }

    static void ListarReposicion(NegocioContext context)
    {
        Console.Clear();

        Console.WriteLine("=================================");
        Console.WriteLine("Listado de articulos para reposición");
        Console.WriteLine("=================================");
        Console.WriteLine();

        var articulosReponer = context
            .Articulos.Include(a => a.Proveedor) //para incluir los datos del proveedor en la consulta
            .Where(a => a.Stock <= a.NivelMinimo) //para filtrar los articulos que necesitan reposición unicamente aquellos cuyo stock es menor o igual al nivel mínimo
            .OrderBy(a => a.ArticuloID)
            .ToList();

        if (articulosReponer.Count == 0)
        {
            Console.WriteLine("No hay artículos que necesiten reposición.");
            Pausa();
            return;
        }
        else
        {
            string carpeta = "Archivos";

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            //-- Guardar el listado en un archivo de texto que se guardará en la carpeta "Archivos" -->

            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string fechaHoraArchivo = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string ruta = Path.Combine(carpeta, "ArticulosAReponer_" + fechaHoraArchivo + ".txt");

            //-- StreamWriter para escribir en el archivo -->
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                //-- Escribir el encabezado en el archivo -->
                writer.WriteLine("Artículos que necesitan reposición:");
                writer.WriteLine("-----------------------------------");
                writer.WriteLine($"Fecha y hora: {fechaActual}");
                writer.WriteLine();

                Console.WriteLine(
                    $"{"ID", -5}"
                        + $"{"DETALLE", -20}"
                        + $"{"PRECIO", 8}"
                        + $"{"STOCK", 10}"
                        + $"{"NIVEL MÍNIMO", 15}"
                        + $"{"PROVEEDOR", 15}"
                        + $"{"EMAIL", 23}"
                );

                Console.WriteLine(new string('-', 120));

                foreach (var articulo in articulosReponer)
                {
                    //-- Escribir los detalles del artículo en el archivo -->
                    writer.WriteLine($"ID: {articulo.ArticuloID}  ");
                    writer.WriteLine($"Detalle: {articulo.Detalle}  ");
                    writer.WriteLine($"Precio: ${articulo.Precio}  ");
                    writer.WriteLine($"Stock: {articulo.Stock}  ");
                    writer.WriteLine($"Nivel mínimo: {articulo.NivelMinimo}  ");
                    writer.WriteLine($"Proveedor: {articulo.Proveedor.Nombre}  ");
                    writer.WriteLine($"Email: {articulo.Proveedor.Email}");
                    writer.WriteLine("-----------------------------------");

                    Console.WriteLine(
                        $"{articulo.ArticuloID, -5}"
                            + $"{articulo.Detalle, -20}"
                            + $"{articulo.Precio, 8:C0}"
                            + $"{articulo.Stock, 8}"
                            + $"{articulo.NivelMinimo, 10}"
                            + $"{articulo.Proveedor.Nombre, 33}"
                            + $"{articulo.Proveedor.Email, 37}"
                    );
                }
                writer.WriteLine();
                writer.WriteLine("Fin del listado.");

                Console.WriteLine();
                Console.WriteLine("Fin del listado.");
            }

            Console.WriteLine();
            Console.WriteLine("Archivo generado correctamente.");
            Console.WriteLine("Ubicación: carpeta Archivos");
            Console.WriteLine(
                "Puede abrirlo para ver el listado completo de artículos que necesitan reposición."
            );
        }
        Pausa();
    }

    static void Pausa()
    {
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }
}
