using GestionProductos.DAL;
using GestionProductos.Entidades;

namespace GestionProductos.BLL
{
    public class ProductoBLL
    {
        private readonly ProductoDAL _productoDAL;

        public ProductoBLL(string connectionString)
        {
            _productoDAL = new ProductoDAL(connectionString);
        }

        public List<Producto> ObtenerTodos()
        {
            return _productoDAL.ObtenerTodos();
        }

        public Producto ObtenerPorId(int id)
        {
            return _productoDAL.ObtenerPorId(id);
        }

        public void Insertar(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                throw new Exception("El nombre del producto es obligatorio.");
            }

            if (producto.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0.");
            }

            if (producto.Stock < 0)
            {
                throw new Exception("El stock no puede ser negativo.");
            }

            _productoDAL.Insertar(producto);
        }

        public void Actualizar(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                throw new Exception("El nombre del producto es obligatorio.");
            }

            if (producto.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0.");
            }

            if (producto.Stock < 0)
            {
                throw new Exception("El stock no puede ser negativo.");
            }

            _productoDAL.Actualizar(producto);
        }

        public void Eliminar(int id)
        {
            _productoDAL.Eliminar(id);
        }

        // =======================================================
        // FUNCIONES STATEFUL (Con estado)
        // =======================================================

        /// <summary>
        /// Stateful 1: Modifica la BD persistiendo el nuevo nivel de inventario.
        /// </summary>
        public void ModificarStockStateful(int id, int ajusteCantidad)
        {
            var producto = ObtenerPorId(id);
            if (producto == null) throw new Exception("Producto no encontrado.");

            int nuevoStock = producto.Stock + ajusteCantidad;
            if (nuevoStock < 0) throw new Exception("El stock resultante no puede ser negativo.");

            producto.Stock = nuevoStock;
            Actualizar(producto);
        }

        /// <summary>
        /// Stateful 2: Modifica una lista de carrito recibida y mantiene el estado acumulativo.
        /// </summary>
        public List<Producto> AgregarAlCarritoStateful(List<Producto> carritoActual, int id)
        {
            var producto = ObtenerPorId(id);
            if (producto != null)
            {
                carritoActual.Add(producto);
            }
            return carritoActual;
        }

        // =======================================================
        // FUNCIONES STATELESS (Sin estado)
        // =======================================================

        /// <summary>
        /// Stateless 1: Retorna el precio con descuento sin guardar ni leer nada externo.
        /// </summary>
        public decimal CalcularPrecioConDescuentoStateless(decimal precioBase, decimal porcentajeDescuento)
        {
            if (precioBase <= 0 || porcentajeDescuento < 0) return precioBase;
            return precioBase - (precioBase * (porcentajeDescuento / 100m));
        }

        /// <summary>
        /// Stateless 2: Determina el nivel de stock según reglas fijas.
        /// </summary>
        public string EvaluarNivelStockStateless(int stock)
        {
            if (stock <= 0) return "Agotado";
            if (stock <= 5) return "Crítico";
            if (stock <= 15) return "Moderado";
            return "Óptimo";
        }
    }
}
