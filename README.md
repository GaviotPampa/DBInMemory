# Negocio Los Dos Chinos

Aplicación de consola desarrollada en **C# y .NET 8** para la gestión básica de un negocio de electrodomésticos.

El proyecto utiliza **Entity Framework Core** para trabajar con los datos y una base de datos **InMemory**, permitiendo gestionar artículos, proveedores y ventas.

## 📌 Descripción del proyecto

El sistema permite:

* Registrar proveedores.
* Registrar artículos.
* Asociar cada artículo con un proveedor.
* Realizar ventas.
* Actualizar el stock automáticamente después de una venta.
* Consultar el listado de artículos.
* Detectar artículos que necesitan reposición.
* Mostrar información del proveedor asociado a cada artículo.
* Generar un archivo `.txt` con el listado de artículos que necesitan reposición.

## 🛠️ Tecnologías utilizadas

* **C#**
* **.NET 8**
* **Entity Framework Core**
* **Base de datos InMemory**
* **Visual Studio Code**

## 🗂️ Estructura del proyecto

La estructura principal del proyecto es:

```text
NegocioLosDosChinos/
│
├── Data/
│   └── NegocioContext.cs
│
├── Models/
│   ├── Articulo.cs
│   ├── Proveedor.cs
│   └── Venta.cs
│
├── Archivos/
│   └── ArticulosAReponer_*.txt
│
├── Program.cs
└── NegocioLosDosChinos.csproj
```

### Data

Contiene `NegocioContext.cs`, que representa el contexto de Entity Framework Core.

En este contexto se definen los `DbSet` correspondientes a:

* `Articulos`
* `Ventas`
* `Proveedores`

Además, se configuran las relaciones entre las entidades.

### Models

Contiene las clases que representan las entidades del sistema:

* `Articulo`
* `Proveedor`
* `Venta`

### Archivos

Esta carpeta se crea automáticamente cuando se genera el listado de reposición.

El archivo generado contiene información de los artículos que necesitan ser repuestos.

## 🔗 Relaciones entre entidades

El proyecto utiliza relaciones entre las entidades mediante Entity Framework Core.

### Proveedor → Artículos

Un proveedor puede tener varios artículos y cada artículo pertenece a un proveedor.

```text
Proveedor
    │
    └─── 1 ──────── N ─── Articulo
```

La relación se establece mediante `ProveedorID`.

### Artículo → Ventas

Un artículo puede aparecer en varias ventas y cada venta corresponde a un artículo.

```text
Articulo
    │
    └─── 1 ──────── N ─── Venta
```

La relación se establece mediante `ArticuloID`.

Estas relaciones están configuradas en `NegocioContext` mediante `HasOne`, `WithMany` y `HasForeignKey`.

##  Base de datos

El proyecto utiliza una base de datos **InMemory** de Entity Framework Core.

La base de datos se configura con:

```csharp
optionsBuilder.UseInMemoryDatabase("Negocio");
```

Esto significa que los datos se almacenan en memoria mientras se ejecuta la aplicación.

## 📦 Datos iniciales

Al iniciar el programa se cargan automáticamente proveedores y artículos de prueba.

### Proveedores

Se cargan tres proveedores:

1. Electro Hogar
2. Distribuidora Central
3. Electrodomésticos del Sur

### Artículos

Se cargan cinco artículos:

| ID | Artículo                    |   Precio | Stock | Nivel mínimo |
| -: | --------------------------- | -------: | ----: | -----------: |
|  1 | Televisor Smart 50 pulgadas | $500.000 |    10 |            3 |
|  2 | Heladera No Frost           | $750.000 |     2 |            5 |
|  3 | Lavarropas Automático       | $620.000 |     8 |            3 |
|  4 | Microondas                  | $180.000 |     1 |            4 |
|  5 | Aspiradora                  | $150.000 |     6 |            2 |

##  Menú principal

Al ejecutar el programa se muestra el siguiente menú:

```text
=================================
       LOS DOS CHINOS
=================================
1. Realizar venta
2. Listar artículos
3. Generar listado de reposición
0. Salir
=================================
```

### 1. Realizar venta

Permite seleccionar un artículo mediante su ID e ingresar la cantidad a vender.

El sistema verifica:

* Que el ID ingresado sea válido.
* Que el artículo exista.
* Que la cantidad sea válida.
* Que la cantidad sea mayor o igual a 1.
* Que la cantidad no supere el stock disponible.

Al confirmar la venta:

* Se registra una nueva venta.
* Se calcula el monto total.
* Se descuenta la cantidad vendida del stock.
* Se guardan los cambios en el contexto.

### 2. Listar artículos

Muestra los artículos registrados, incluyendo:

* ID
* Detalle
* Precio
* Stock

Los artículos se muestran ordenados por ID.

### 3. Generar listado de reposición

El sistema busca los artículos cuyo stock sea **menor o igual al nivel mínimo establecido**.

Para obtener también los datos del proveedor se utiliza una consulta con `Include`.

Se muestra:

* ID
* Detalle
* Precio
* Stock
* Nivel mínimo
* Proveedor
* Email

Además, se genera automáticamente un archivo de texto dentro de la carpeta:

```text
Archivos/
```

El nombre del archivo incluye la fecha y hora de generación:

```text
ArticulosAReponer_YYYYMMDD_HHMMSS.txt
```

##  Archivo de reposición

El archivo `.txt` generado contiene:

```text
Artículos que necesitan reposición:
-----------------------------------
Fecha y hora: ...

ID: ...
Detalle: ...
Precio: ...
Stock: ...
Nivel mínimo: ...
Proveedor: ...
Email: ...
-----------------------------------
```

De esta manera se obtiene un listado que puede utilizarse para identificar los productos que necesitan ser repuestos.

## ▶ Cómo ejecutar el proyecto

### Requisitos

Tener instalado:

* .NET 8 SDK
* Visual Studio Code u otro entorno compatible con .NET

### Ejecutar

Abrir una terminal dentro de la carpeta del proyecto y ejecutar:

```bash
dotnet restore
```

Luego:

```bash
dotnet run
```

##  Objetivos del proyecto

El proyecto tiene como objetivo aplicar los conceptos trabajados durante la cursada:

* Programación Orientada a Objetos.
* Clases y objetos.
* Relaciones entre entidades.
* Entity Framework Core.
* `DbContext` y `DbSet`.
* Claves primarias y foráneas.
* Consultas con LINQ.
* Manejo de colecciones.
* Validación de datos ingresados por el usuario.
* Actualización de stock.
* Persistencia de información durante la ejecución.
* Generación y escritura de archivos de texto.

##  Proyecto

**Negocio Los Dos Chinos**

Proyecto desarrollado como trabajo práctico de Programación Orientada a Objetos.
