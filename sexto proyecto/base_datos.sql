
-- Habilita el trabajo con llaves foráneas
PRAGMA foreign_keys = ON;


-- Tabla Pedido
CREATE TABLE IF NOT EXISTS Pedido (
    id_pedido INTEGER PRIMARY KEY AUTOINCREMENT,
    fecha TEXT NOT NULL,
    direccion_entrega TEXT NOT NULL,
    estado TEXT NOT NULL
);


-- Tabla Producto
CREATE TABLE IF NOT EXISTS Producto (
    id_producto INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL,
    precio REAL NOT NULL,
    stock INTEGER NOT NULL
);


-- Tabla PedidoProducto
CREATE TABLE IF NOT EXISTS PedidoProducto (
    id_pedido INTEGER NOT NULL,
    id_producto INTEGER NOT NULL,
    cantidad INTEGER NOT NULL,
    precio_unitario REAL NOT NULL,

    FOREIGN KEY (id_pedido)
        REFERENCES Pedido(id_pedido),

    FOREIGN KEY (id_producto)
        REFERENCES Producto(id_producto),

    PRIMARY KEY (id_pedido, id_producto)
);


-- Insertar productos
INSERT INTO Producto (nombre, precio, stock)
VALUES
('Hamburguesa', 250, 20),
('Pizza', 500, 15),
('Refresco', 75, 50),
('Sandwich', 150, 25),
('Jugo natural', 100, 30);


-- Insertar pedidos
INSERT INTO Pedido (fecha, direccion_entrega, estado)
VALUES
('2026-11-13', 'Villa Hermosa', 'Pendiente'),
('2026-11-13', 'La Romana', 'Entregado'),
('2026-11-13', 'San Carlos', 'En camino'),
('2026-11-13', 'Centro de la ciudad', 'Pendiente'),
('2026-11-13', 'Savica', 'Entregado');


-- Insertar detalles de los pedidos
INSERT INTO PedidoProducto
(id_pedido, id_producto, cantidad, precio_unitario)
VALUES
(1, 1, 2, 250),
(1, 3, 2, 75),
(2, 2, 1, 500),
(2, 3, 1, 75),
(3, 4, 2, 150),
(4, 5, 3, 100),
(5, 1, 1, 250);


-- Consultar todos los pedidos y sus productos
SELECT
    Pedido.id_pedido,
    Pedido.fecha,
    Pedido.direccion_entrega,
    Pedido.estado,
    Productoucto;
.nombre AS producto,
    PedidoProducto.cantidad,
    PedidoProducto.precio_unitario
FROM Pedido
INNER JOIN PedidoProducto
ON Pedido.id_pedido = PedidoProducto.id_pedido
INNER JOIN Producto
ON PedidoProducto.id_producto = Producto.id_prod