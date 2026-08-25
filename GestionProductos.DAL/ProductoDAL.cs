using Microsoft.Data.SqlClient;
using GestionProductos.Entidades;

namespace GestionProductos.DAL
{
    public class ProductoDAL
    {
        private readonly string _connectionString;

        public ProductoDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            using SqlConnection connection = new SqlConnection(_connectionString);

            string query = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos";

            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(reader["Precio"]),
                    Stock = Convert.ToInt32(reader["Stock"])
                });
            }

            return productos;
        }

        public Producto ObtenerPorId(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            string query = @"SELECT Id, Nombre, Descripcion, Precio, Stock
                             FROM Productos
                             WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Producto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(reader["Precio"]),
                    Stock = Convert.ToInt32(reader["Stock"])
                };
            }

            return null;
        }

        public void Insertar(Producto producto)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Productos
                            (Nombre, Descripcion, Precio, Stock)
                            VALUES
                            (@Nombre, @Descripcion, @Precio, @Stock)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Stock", producto.Stock);

            connection.Open();

            command.ExecuteNonQuery();
        }

        public void Actualizar(Producto producto)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            string query = @"UPDATE Productos
                             SET Nombre = @Nombre,
                                 Descripcion = @Descripcion,
                                 Precio = @Precio,
                                 Stock = @Stock
                             WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", producto.Id);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Stock", producto.Stock);

            connection.Open();

            command.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            string query = "DELETE FROM Productos WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            command.ExecuteNonQuery();
        }
    }
}