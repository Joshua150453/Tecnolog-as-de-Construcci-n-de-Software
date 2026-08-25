# 📦 Sistema de Gestión de Productos (Arquitectura N-Capas)

Sistema web completo para la gestión de productos (CRUD) desarrollado en **.NET 10**, implementando una **Arquitectura N-Capas** desacoplada mediante **ASP.NET Core MVC**, **ADO.NET** y **SQL Server**.

---

## 📐 Arquitectura del Sistema

El proyecto está diseñado siguiendo el patrón de arquitectura en N-Capas para garantizar la separación de responsabilidades, mantenibilidad y escalabilidad del código.

```text
               [ USUARIO / NAVEGADOR ]
                          │
                          ▼
        ┌──────────────────────────────────┐
        │     CAPA DE PRESENTACIÓN (WEB)   │
        │     ASP.NET Core MVC / Razor     │
        └─────────────────┬────────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │  CAPA DE LÓGICA DE NEGOCIO (BLL) │
        │     Reglas de Negocio / Valid.   │
        └─────────────────┬────────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │   CAPA DE ACCESO A DATOS (DAL)   │
        │     ADO.NET (SqlClient)          │
        └─────────────────┬────────────────┘
                          │
                          ▼
        ┌──────────────────────────────────┐
        │          BASE DE DATOS           │
        │           SQL Server             │
        └──────────────────────────────────┘
```

### Descripción de las Capas

- **GestionProductos.Entidades**: Contiene la clase de dominio `Producto.cs`. Es una capa transversal utilizada por las demás capas para transportar la información estructurada.

- **GestionProductos.DAL (Data Access Layer)**: Implementa ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`) para conectarse directamente a SQL Server y ejecutar consultas/procedimientos.

- **GestionProductos.BLL (Business Logic Layer)**: Contiene la lógica de negocio y las validaciones de los datos (ej. verificación de precios > 0, nombres no nulos y stock no negativo) antes de procesarlos en la BD.

- **GestionProductos.Web (Presentation Layer)**: Desarrollada con ASP.NET Core MVC. Recibe las peticiones HTTP mediante `ProductoController.cs` y renderiza las vistas con Razor (`.cshtml`).

---

## 🚀 Tecnologías Utilizadas

- **Lenguaje**: C# (.NET 10 SDK)
- **Framework Web**: ASP.NET Core MVC
- **Acceso a Datos**: ADO.NET (Microsoft.Data.SqlClient)
- **Base de Datos**: SQL Server
- **Frontend**: Razor Views, HTML5, CSS3, Bootstrap 5

---

## 🛠️ Configuración e Instalación

### Prerrequisitos

- .NET 10 SDK
- SQL Server
- SQL Server Management Studio (SSMS) o sqlcmd

### 1. Base de Datos

Ejecutar el siguiente script en tu instancia de SQL Server para crear la base de datos y la tabla correspondiente:

```sql
CREATE DATABASE GestionProductos;
GO

USE GestionProductos;
GO

CREATE TABLE Productos
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(250),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL
);
GO

-- Datos de prueba opcionales
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock)
VALUES
('Laptop', 'Laptop Lenovo 16GB RAM', 2500.00, 10),
('Mouse', 'Mouse inalámbrico ergonómico', 50.00, 25),
('Teclado', 'Teclado mecánico RGB', 150.00, 15);
GO
```

### 2. Cadena de Conexión

Asegurarse de actualizar la cadena de conexión en el archivo `GestionProductos.Web/appsettings.json` según el entorno local:

```json
{
  "ConnectionStrings": {
    "ConexionSQL": "Server=localhost;Database=GestionProductos;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> **Nota**: Si utiliza SQL Server Express, cambie `localhost` por `localhost\SQLEXPRESS`.

### 3. Ejecución del Proyecto

Clone el repositorio:

```bash
git clone https://github.com/Joshua150453/Tecnolog-as-de-Construcci-n-de-Software.git
cd Tecnolog-as-de-Construcci-n-de-Software
```

Restaure las dependencias y compile la solución:

```bash
dotnet build
```

Inicie la aplicación web:

```bash
dotnet run --project GestionProductos.Web
```

Abra su navegador e ingrese a la URL mostrada en la consola (ej. `http://localhost:5231`).

---

## 📋 Funcionalidades (CRUD)

- [x] **Consultar (Read)**: Listado general de productos registrados en la base de datos.
- [x] **Registrar (Create)**: Formulario para ingresar nuevos productos con validación de datos en tiempo real.
- [x] **Editar (Update)**: Modificación de nombre, descripción, precio y stock de productos existentes.
- [x] **Eliminar (Delete)**: Borrado de productos en la base de datos con confirmación de seguridad.

---

## 👤 Autor

**Joshua David Ortiz Rosas** - [Joshua150453](https://github.com/Joshua150453)
