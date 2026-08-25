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
    }
}