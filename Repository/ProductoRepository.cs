using System.Data.SqlClient;
using RepositorioDTO.Models;
using System.Data;

namespace RepositorioDTO.Repository
{
    public class ProductoRepository
    {
        private readonly string _conexion;

        public ProductoRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("conexion");
        }

        public IEnumerable<Producto> ObtenerProductos()
        {
            using (SqlConnection conexion = new(_conexion))
            {
                using (SqlCommand cmd = new("ObtenerProductos", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Producto> productos = new List<Producto>();
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                            };
                            productos.Add(producto);
                        }
                        return productos;
                    }
                }
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            using (SqlConnection conexion = new(_conexion))
            {
                using (SqlCommand cmd = new("ActualizarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", producto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AgregarProducto(Producto producto)
        {
            using (SqlConnection conexion = new(_conexion))
            {
                using (SqlCommand cmd = new("AgregarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarProducto(int id)
        {
            using (SqlConnection conexion = new(_conexion))
            {
                using (SqlCommand cmd = new("EliminarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}