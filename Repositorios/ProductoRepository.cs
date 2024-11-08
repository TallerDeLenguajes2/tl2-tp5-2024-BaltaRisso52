using Microsoft.Data.Sqlite;
public class ProductoRepository
{
    static string cadenaConexion = @"Data Source=db/Tienda.db;cache=Shared";
    public static void CrearProducto(ProductoPostDto producto)
    {
        string consulta = "INSERT INTO Productos (Descripcion, Precio) VALUES (@descripcion, @precio)";
        string descripcion = producto.Descripcion;
        int precio = producto.Precio;

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(consulta, connection);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@precio", precio);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public static void ModificarProducto(int id, Producto producto)
    {
        string consulta = "UPDATE Productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id";

        string descripcion = producto.Descripcion;
        int precio = producto.Precio;

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@precio", precio);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }


    }

    public static List<Producto> GetProductos()
    {

        List<Producto> productos = new();
        string consulta = "SELECT * FROM Productos";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string descripcion = reader.GetString(1); // reader["Descripcion"].ToString();
                    int precio = reader.GetInt32(2);

                    Producto producto = new Producto(id, descripcion, precio);
                    productos.Add(producto);
                }
            }
            connection.Close();
        }

        return productos;
    }

    public static Producto? GetProducto(int id)
    {
        string consulta = "SELECT * FROM Productos WHERE idProducto = @id";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);
            command.Parameters.AddWithValue("@id", id);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Producto producto = new Producto(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    connection.Close();
                    return producto;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            
        }

    }

    public static void EliminarProducto(int id){
        string consulta = "DELETE FROM Productos WHERE idProducto = @id";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta,connection);

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}