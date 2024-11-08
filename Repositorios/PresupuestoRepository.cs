using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    static string cadenaConexion = @"Data Source=db/Tienda.db;cache=Shared";

    public static void CrearPresupuesto(Presupuesto presupuesto)
    {

        string consulta = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombre, @fecha)";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", presupuesto.NombreDestinatario);
            command.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public static List<Presupuesto> GetPresupuestos()
    {
        List<Presupuesto> presupuestos = new();
        string consulta = "SELECT * FROM Presupuestos";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Presupuesto presupuesto = new Presupuesto(id, reader.GetString(1), GetPresupuestoDetalles(id));
                    presupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return presupuestos;
    }

    public static Presupuesto? GetPresupuesto(int id)
    {
        string consulta = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);

            command.Parameters.AddWithValue("@id", id);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Presupuesto presupuesto = new Presupuesto(reader.GetInt32(0), reader.GetString(1), GetPresupuestoDetalles(id));
                    connection.Close();
                    return presupuesto;
                }
                else
                {
                    connection.Close();
                    return null;
                }

            }

        }
    }

    public static List<PresupuestoDetalle> GetPresupuestoDetalles(int id)
    {
        List<PresupuestoDetalle> presupuestos = new();
        string consulta = "SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = @id";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta, connection);

            command.Parameters.AddWithValue("@id", id);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PresupuestoDetalle presupuestoDetalle = new PresupuestoDetalle(ProductoRepository.GetProducto(reader.GetInt32(1)), reader.GetInt32(2));

                    presupuestos.Add(presupuestoDetalle);

                }
            }
            connection.Close();
        }

        return presupuestos;
    }

    public static bool AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        Producto? producto = ProductoRepository.GetProducto(idProducto);
        Presupuesto? presupuesto = GetPresupuesto(idPresupuesto);
        if (producto is not null && presupuesto is not null)
        {
            string consulta = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(consulta, connection);
                command.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                command.Parameters.AddWithValue("@idProducto", idProducto);
                command.Parameters.AddWithValue("@cantidad", cantidad);

                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public static void EliminarPresupuestoPorId(int id){
        string consulta1 = "DELETE FROM Presupuestos WHERE idPresupuesto = @idP;";
        string consulta2 = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id;";

        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(consulta1,connection);
            SqliteCommand command2 = new SqliteCommand(consulta2,connection);

            command.Parameters.AddWithValue("@idP", id);
            command2.Parameters.AddWithValue("@id", id);

            command2.ExecuteNonQuery();
            command.ExecuteNonQuery();

            connection.Close();
        }
    }


}