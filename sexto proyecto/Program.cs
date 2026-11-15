
using System;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main(string[] args)
    {
        string cadena = "Data Source=delivery.db";

        using (var conexion = new SqliteConnection(cadena))
        {
            conexion.Open();

            // ACTIVAR LLAVES FORANEAS
            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "PRAGMA foreign_keys = ON;";
                cmd.ExecuteNonQuery();
            }

            // CREAR TABLA PEDIDO
            string pedido = @"
            CREATE TABLE IF NOT EXISTS Pedido (
                id_pedido INTEGER PRIMARY KEY AUTOINCREMENT,
                fecha TEXT NOT NULL,
                direccion_entrega TEXT NOT NULL,
                estado TEXT NOT NULL
            );";

            // CREAR TABLA PRODUCTO
            string producto = @"
            CREATE TABLE IF NOT EXISTS Producto (
                id_producto INTEGER PRIMARY KEY AUTOINCREMENT,
                nombre TEXT NOT NULL,
                precio REAL NOT NULL,
                stock INTEGER NOT NULL
            );";

            // CREAR TABLA DETALLEPEDIDO
            string detalle = @"
            CREATE TABLE IF NOT EXISTS DetallePedido (
                id_detalle INTEGER PRIMARY KEY AUTOINCREMENT,
                id_pedido INTEGER NOT NULL,
                id_producto INTEGER NOT NULL,
                cantidad INTEGER NOT NULL,
                subtotal REAL NOT NULL,
                FOREIGN KEY (id_pedido)
                    REFERENCES Pedido(id_pedido),
                FOREIGN KEY (id_producto)
                    REFERENCES Producto(id_producto)
            );";

            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = pedido + producto + detalle;
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Tablas creadas correctamente.");

            // INSERTAR PRODUCTOS
            string insertar = @"
            INSERT OR IGNORE INTO Producto
            (id_producto, nombre, precio, stock)
            VALUES
            (1, 'Hamburguesa', 350, 20);

            INSERT OR IGNORE INTO Producto
            (id_producto, nombre, precio, stock)
            VALUES
            (2, 'Pizza', 500, 15);

            INSERT OR IGNORE INTO Producto
            (id_producto, nombre, precio, stock)
            VALUES
            (3, 'Refresco', 100, 50);";

            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = insertar;
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Productos insertados correctamente.");

            // MOSTRAR TABLA PEDIDO
            Console.WriteLine("\n===== TABLA PEDIDO =====");

            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Pedido;";

                using (var lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Console.WriteLine(
                            lector.GetInt32(0) + " - " +
                            lector.GetString(1) + " - " +
                            lector.GetString(2) + " - " +
                            lector.GetString(3));
                    }
                }
            }

            // MOSTRAR TABLA PRODUCTO
            Console.WriteLine("\n===== TABLA PRODUCTO =====");

            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Producto;";

                using (var lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Console.WriteLine(
                            lector.GetInt32(0) + " - " +
                            lector.GetString(1) + " - RD$" +
                            lector.GetDouble(2) + " - Stock: " +
                            lector.GetInt32(3));
                    }
                }
            }

            // MOSTRAR TABLA DETALLEPEDIDO
            Console.WriteLine("\n===== TABLA DETALLEPEDIDO =====");

            using (var cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM DetallePedido;";

                using (var lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Console.WriteLine(
                            lector.GetInt32(0) + " - " +
                            lector.GetInt32(1) + " - " +
                            lector.GetInt32(2) + " - " +
                            lector.GetInt32(3) + " - RD$" +
                            lector.GetDouble(4));
                    }
                }
            }

            Console.WriteLine("\nPrograma ejecutado correctamente.");
        }

        Console.ReadKey();
    }
}
