-- 1. CREAR BASE DE DATOS Y LOGIN
USE master;
GO

DROP DATABASE IF EXISTS LabRestaurante;
GO

--DROP USER usrrestaurante;
DROP LOGIN usrrestaurante;
GO

CREATE DATABASE LabRestaurante;
GO

CREATE LOGIN [usrrestaurante] WITH PASSWORD = N'123456',
    DEFAULT_DATABASE = [LabRestaurante],
    CHECK_EXPIRATION = OFF,
    CHECK_POLICY = ON;
GO

USE LabRestaurante;
GO

CREATE USER [usrrestaurante] FOR LOGIN [usrrestaurante];
GO
ALTER ROLE [db_owner] ADD MEMBER [usrrestaurante];
GO

-- 2. ELIMINAR OBJETOS EXISTENTES

DROP TABLE IF EXISTS PagoVenta;
DROP TABLE IF EXISTS MetodoPago;
DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS DetalleCompra;
DROP TABLE IF EXISTS Compra;
DROP TABLE IF EXISTS Proveedor;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Mesa;
DROP TABLE IF EXISTS Sala;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Categoria;

-- Eliminar Procedimientos
DROP PROC IF EXISTS paCategoriaListar;
DROP PROC IF EXISTS paProductoListar;
DROP PROC IF EXISTS paClienteListar;
DROP PROC IF EXISTS paEmpleadoListar;
DROP PROC IF EXISTS paUsuarioListar;
DROP PROC IF EXISTS paVentaListarPorFecha;
DROP PROC IF EXISTS paDetalleVentaListar;
GO

-- 3. BASE DE DATOS ERP/POS PARA RESTAURANTE
-- 3.1. MÓDULO DE INVENTARIO Y CONFIGURACIÓN DE PRODUCTOS
CREATE TABLE Categoria (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Producto (
    id INT PRIMARY KEY IDENTITY(1,1),
    idCategoria INT NOT NULL,
    codigo VARCHAR(20) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(250) NULL,
    imagenUrl VARCHAR(300) NULL,
    stock DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (stock >= 0),
    precioVenta DECIMAL(10,2) NOT NULL CHECK (precioVenta > 0),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Producto_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id)
);

-- 3.2. MÓDULO DE SALAS Y MESAS (INFRAESTRUCTURA DEL RESTAURANTE)
CREATE TABLE Sala (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE, -- Ejemplo: 'Planta Alta', 'Terraza'
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Mesa (
    id INT PRIMARY KEY IDENTITY(1,1),
    idSala INT NOT NULL,
    numero VARCHAR(10) NOT NULL,        -- Ejemplo: 'Mesa 1', 'Mesa 2'
    capacidad INT NOT NULL DEFAULT 4,
    estadoMesa VARCHAR(20) NOT NULL DEFAULT 'DISPONIBLE', -- DISPONIBLE, OCUPADA, RESERVADA
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Mesa_Sala FOREIGN KEY (idSala) REFERENCES Sala(id)
);

-- 3.3. MÓDULO DE ACTORES (CLIENTES, EMPLEADOS Y USUARIOS)
CREATE TABLE Cliente (
    id INT PRIMARY KEY IDENTITY(1,1),
    ciNit VARCHAR(20) NOT NULL UNIQUE,
    razonSocial VARCHAR(100) NOT NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Empleado (
    id INT PRIMARY KEY IDENTITY(1,1),
    cedulaIdentidad VARCHAR(12) NOT NULL UNIQUE,
    nombres VARCHAR(50) NOT NULL,
    primerApellido VARCHAR(50) NULL,
    segundoApellido VARCHAR(50) NULL,
    fechaNacimiento DATE NOT NULL,
    direccion VARCHAR(250) NOT NULL,
    celular BIGINT NOT NULL,
    cargo VARCHAR(50) NOT NULL, -- Ejemplo: 'Mesero', 'Cajero', 'Administrador'
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Usuario (
    id INT PRIMARY KEY IDENTITY(1,1),
    idEmpleado INT NOT NULL,
    usuario VARCHAR(20) NOT NULL UNIQUE,
    clave VARCHAR(250) NOT NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Usuario_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
);

-- 3.4. MÓDULO DE COMPRAS Y PROVEEDORES (ABASTECIMIENTO / ERP)
CREATE TABLE Proveedor (
    id INT PRIMARY KEY IDENTITY(1,1),
    nit VARCHAR(20) NOT NULL UNIQUE,
    razonSocial VARCHAR(100) NOT NULL,
    contacto VARCHAR(50) NULL,
    telefono BIGINT NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Compra (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idProveedor INT NOT NULL,
    idUsuario INT NOT NULL, -- Usuario que registra el ingreso de mercadería
    nroFacturaNota VARCHAR(20) NOT NULL,
    fechaCompra DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    totalCompra DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (totalCompra >= 0),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Compra_Proveedor FOREIGN KEY (idProveedor) REFERENCES Proveedor(id),
    CONSTRAINT fk_Compra_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id)
);

CREATE TABLE DetalleCompra (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idCompra BIGINT NOT NULL,
    idProducto INT NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL CHECK (cantidad > 0),
    precioCosto DECIMAL(10,2) NOT NULL CHECK (precioCosto > 0),
    subtotal AS (cantidad * precioCosto) PERSISTED,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_DetalleCompra_Compra FOREIGN KEY (idCompra) REFERENCES Compra(id),
    CONSTRAINT fk_DetalleCompra_Producto FOREIGN KEY (idProducto) REFERENCES Producto(id)
);

-- 3.5. TABLAS DE VENTAS (CON EXTENSIONES PARA RESTAURANTE)
CREATE TABLE Venta (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL, -- Cajero/Mesero que procesa la transacción
    idMesa INT NULL,       -- NULL significa que es Pedido para llevar o Delivery
    tipoPedido VARCHAR(20) NOT NULL DEFAULT 'MESA', -- Valores: MESA, LLEVAR, DELIVERY
    numeroTransaccion AS ('VEN-' + CAST(id AS VARCHAR(10))),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Venta_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(id),
    CONSTRAINT fk_Venta_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    CONSTRAINT fk_Venta_Mesa FOREIGN KEY (idMesa) REFERENCES Mesa(id)
);

CREATE TABLE DetalleVenta (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idVenta BIGINT NOT NULL,
    idProducto INT NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL CHECK (cantidad > 0),
    precioUnitario DECIMAL(10,2) NOT NULL CHECK (precioUnitario > 0),
    total AS (cantidad * precioUnitario) PERSISTED,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_DetalleVenta_Venta FOREIGN KEY (idVenta) REFERENCES Venta(id),
    CONSTRAINT fk_DetalleVenta_Producto FOREIGN KEY (idProducto) REFERENCES Producto(id)
);

-- 3.6. MÓDULO DE PAGOS MÚLTIPLES
CREATE TABLE MetodoPago (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE, -- Ejemplo: 'Efectivo', 'Tarjeta', 'QR', 'PedidosYa'
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE PagoVenta (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idVenta BIGINT NOT NULL,
    idMetodoPago INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL CHECK (monto > 0),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_PagoVenta_Venta FOREIGN KEY (idVenta) REFERENCES Venta(id),
    CONSTRAINT fk_PagoVenta_Metodo FOREIGN KEY (idMetodoPago) REFERENCES MetodoPago(id)
);
GO

-- 4. PROCEDIMIENTOS ALMACENADOS

CREATE PROC paCategoriaListar @parametro VARCHAR(50)
AS
    SELECT c.id, c.nombre, c.usuarioRegistro, c.fechaRegistro, c.estado
    FROM Categoria c
    WHERE c.estado <> -1
      AND c.nombre LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY c.nombre ASC;
GO

CREATE PROC paProductoListar @parametro VARCHAR(50)
AS
    SELECT p.id, p.idCategoria, c.nombre AS categoria, p.codigo, 
           p.nombre, p.descripcion, p.stock, p.precioVenta, 
           p.usuarioRegistro, p.fechaRegistro, p.estado
    FROM Producto p
    INNER JOIN Categoria c ON c.id = p.idCategoria
    WHERE p.estado <> -1
      AND (p.nombre + p.codigo + ISNULL(p.descripcion,'') + c.nombre) 
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY p.estado DESC, p.nombre ASC;
GO

CREATE PROC paClienteListar @parametro VARCHAR(50)
AS
    SELECT c.id, c.ciNit, c.razonSocial, 
           c.usuarioRegistro, c.fechaRegistro, c.estado
    FROM Cliente c
    WHERE c.estado <> -1
      AND (c.ciNit + c.razonSocial) LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY c.razonSocial ASC;
GO

CREATE PROC paEmpleadoListar @parametro VARCHAR(50)
AS
    SELECT e.id, e.cedulaIdentidad, e.nombres, 
           ISNULL(e.primerApellido, '') AS primerApellido,
           ISNULL(e.segundoApellido, '') AS segundoApellido,
           e.fechaNacimiento, e.direccion, e.celular, e.cargo,
           ISNULL(e.usuarioRegistro, '') AS usuarioRegistro,
           ISNULL(e.fechaRegistro, GETDATE()) AS fechaRegistro,
           ISNULL(u.id, 0) AS idUsuario,
           ISNULL(u.usuario, '') AS usuario,
           e.estado
    FROM Empleado e
    LEFT JOIN Usuario u ON e.id = u.idEmpleado
    WHERE e.estado <> -1
      AND (e.cedulaIdentidad + e.nombres + ISNULL(e.primerApellido,'') + ISNULL(e.segundoApellido,''))
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY e.nombres, e.primerApellido;
GO

CREATE PROC paUsuarioListar @parametro VARCHAR(50)
AS
    SELECT u.id, u.idEmpleado,
           e.nombres + ' ' + ISNULL(e.primerApellido,'') AS empleado,
           u.usuario, u.usuarioRegistro, u.fechaRegistro, u.estado
    FROM Usuario u
    INNER JOIN Empleado e ON e.id = u.idEmpleado
    WHERE u.estado <> -1
      AND (u.usuario + e.nombres + ISNULL(e.primerApellido,'')) 
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY u.usuario ASC;
GO

CREATE PROCEDURE paVentaListarPorFecha
    @parametro VARCHAR(50),
    @fechaInicio DATETIME = NULL,
    @fechaFin DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.id,
        v.numeroTransaccion,
        c.razonSocial AS cliente,
        -- Concatenación con los nombres de columna reales de tu tabla Empleado
        ISNULL(e.nombres, '') + ' ' + ISNULL(e.primerApellido, '') + ' ' + ISNULL(e.segundoApellido, '') AS Usuario,
        m.id AS mesa,
        v.tipoPedido,
        v.fechaRegistro,
        v.estado,
        v.usuarioRegistro
    FROM Venta v
    INNER JOIN Cliente c ON v.idCliente = c.id
    INNER JOIN Usuario u ON v.idUsuario = u.id
    INNER JOIN Empleado e ON u.idEmpleado = e.id
    LEFT JOIN Mesa m ON v.idMesa = m.id
    WHERE v.estado = 1
      AND (c.razonSocial LIKE '%' + @parametro + '%' OR v.numeroTransaccion LIKE '%' + @parametro + '%')
      AND (@fechaInicio IS NULL OR v.fechaRegistro >= @fechaInicio)
      AND (@fechaFin IS NULL OR v.fechaRegistro <= DATEADD(SECOND, 86399, @fechaFin))
    ORDER BY v.id DESC;
END;
GO

CREATE PROC paDetalleVentaListar @parametro VARCHAR(100)
AS
    SELECT dv.id, dv.idVenta, v.numeroTransaccion,
           p.codigo, p.nombre AS producto,
           dv.cantidad, dv.precioUnitario, dv.total,
           dv.usuarioRegistro, dv.fechaRegistro, dv.estado
    FROM DetalleVenta dv
    INNER JOIN Venta v ON v.id = dv.idVenta
    INNER JOIN Producto p ON p.id = dv.idProducto
    WHERE dv.estado <> -1
      AND (p.nombre + p.codigo + v.numeroTransaccion)
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY dv.id ASC;
GO

CREATE PROCEDURE paProductoTopMasVendidos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 3
        p.id,
        p.nombre,
        p.imagenUrl,
        SUM(dv.cantidad) AS totalVendido
    FROM DetalleVenta dv
    INNER JOIN Venta v ON dv.idVenta = v.id
    INNER JOIN Producto p ON dv.idProducto = p.id
    WHERE v.estado = 1       -- Solo ventas válidas (no anuladas)
      AND dv.estado = 1      -- Detalles válidos
      AND p.estado = 1       -- Productos activos
    GROUP BY p.id, p.nombre, p.imagenUrl
    ORDER BY totalVendido DESC;
END;
GO

-- Para reportes financieros, sumamos el total de cada detalle de venta para obtener el total neto de cada transacción

CREATE PROCEDURE paReporteFinancieroPorFecha
    @parametro VARCHAR(50),
    @fechaInicio DATETIME = NULL,
    @fechaFin DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.id,
        v.numeroTransaccion,
        v.fechaRegistro,
        c.razonSocial AS cliente,
        v.tipoPedido,
        v.estado,
        -- Sumamos el total calculado del detalle de cada venta activa
        ISNULL(SUM(dv.total), 0) AS totalNeto
    FROM Venta v
    INNER JOIN Cliente c ON v.idCliente = c.id
    LEFT JOIN DetalleVenta dv ON v.id = dv.idVenta AND dv.estado = 1
    WHERE v.estado = 1 -- Solo tomamos ventas no anuladas en general
      AND (c.razonSocial LIKE '%' + @parametro + '%' OR v.numeroTransaccion LIKE '%' + @parametro + '%')
      AND (@fechaInicio IS NULL OR v.fechaRegistro >= @fechaInicio)
      AND (@fechaFin IS NULL OR v.fechaRegistro <= DATEADD(SECOND, 86399, @fechaFin))
    GROUP BY 
        v.id, v.numeroTransaccion, v.fechaRegistro, c.razonSocial, v.tipoPedido, v.estado
    ORDER BY 
        v.fechaRegistro DESC;
END;
GO

CREATE PROCEDURE paReporteTopProductosGerencial
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 3
        p.id,
        p.nombre,
        SUM(dv.cantidad) AS totalVendido
    FROM DetalleVenta dv
    INNER JOIN Venta v ON dv.idVenta = v.id
    INNER JOIN Producto p ON dv.idProducto = p.id
    WHERE v.estado = 1       -- Solo ventas válidas (no anuladas)
      AND dv.estado = 1      -- Detalles válidos
      AND p.estado = 1       -- Productos activos
    GROUP BY p.id, p.nombre
    ORDER BY totalVendido DESC;
END;
GO

CREATE PROCEDURE paReporteTipoPedidoGerencial
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.tipoPedido,
        COUNT(v.id) AS cantidadOrdenes,
        ISNULL(SUM(dv.total), 0) AS totalIngresos
    FROM Venta v
    LEFT JOIN DetalleVenta dv ON v.id = dv.idVenta AND dv.estado = 1
    WHERE v.estado = 1 -- Solo ventas válidas
    GROUP BY v.tipoPedido
    ORDER BY totalIngresos DESC;
END;
GO

-- 5. DATOS DE PRUEBA 

-- --- CATEGORÍAS ---
INSERT INTO Categoria (nombre) VALUES ('Platos Fuertes');             -- ID 1
INSERT INTO Categoria (nombre) VALUES ('Postres');                    -- ID 2
INSERT INTO Categoria (nombre) VALUES ('Acompañamientos y Entradas'); -- ID 3
INSERT INTO Categoria (nombre) VALUES ('Bebidas');                    -- ID 4

-- --- INFRAESTRUCTURA ---
INSERT INTO Sala (nombre) VALUES ('Salón Principal'), ('Terraza Exterior');
INSERT INTO Mesa (idSala, numero, capacidad, estadoMesa) VALUES 
(1, 'Mesa 1', 4, 'OCUPADA'),
(1, 'Mesa 2', 2, 'DISPONIBLE'),
(2, 'Mesa 3', 6, 'OCUPADA');

-- --- MÉTODOS DE PAGO ---
INSERT INTO MetodoPago (nombre) VALUES ('Efectivo'), ('Tarjeta de Débito'), ('QR');

-- --- PROVEEDORES Y COMPRAS ---
INSERT INTO Proveedor (nit, razonSocial, contacto, telefono) VALUES ('1029384022', 'Distribuidora Altiplano', 'Pedro Murillo', 60012345);
-- Compra inicial simulada por usuario administrador (Prontamente ID 2)
-- Para que corra lineal, insertamos empleados y usuarios primero

-- --- EMPLEADOS ---
INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('1234567', 'Jhoselin', 'Figueroa', 'Colque', '1990-05-15', 'Av. 6 de Agosto 123', 71234567, 'Mesero');

INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('7654321', 'Elizabeth', 'Diaz', 'Canchari', '1988-03-20', 'Calle Potosí 456', 76543210, 'Administrador');

-- --- USUARIOS ---
INSERT INTO Usuario (idEmpleado, usuario, clave) VALUES (1, 'jhoselin', 'I0HCOO/NSSY6WOS9POP5XW=='); -- ID 1
INSERT INTO Usuario (idEmpleado, usuario, clave) VALUES (2, 'elizabet', 'I0HCOO/NSSY6WOS9POP5XW=='); -- ID 2

-- --- PRODUCTOS (IDs Correlativos Automáticos 1 al 34) ---
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, stock, precioVenta) VALUES
(1, 'PROD-CEV001', 'Ceviche', 'Pescado marinado.', 50.00, 35.00), -- ID 1
(1, 'PROD-LOM002', 'Lomo Saltado', 'Filete de carne salteada.', 50.00, 38.00), -- ID 2
(1, 'PROD-AJI003', 'Ají de Gallina', 'Pollo en crema de ají.', 50.00, 28.00), -- ID 3
(1, 'PROD-ARR005', 'Arroz con Pato', 'Pato marinado en cerveza.', 30.00, 42.00), -- ID 4
(1, 'PROD-CUY006', 'Cuy al Horno', 'Cuy en horno de leña.', 20.00, 65.00), -- ID 5
(1, 'PROD-POL007', 'Pollo a la Brasa', 'Pollo a las brasas.', 100.00, 25.00), -- ID 6
(1, 'PROD-ROC008', 'Rocoto Relleno', 'Rocoto picante relleno.', 40.00, 30.00), -- ID 7
(1, 'PROD-SEC010', 'Seco de Carne', 'Guiso de carne y cilantro.', 50.00, 32.00), -- ID 8
(1, 'PROD-PAC011', 'Pachamanca', 'Carnes bajo tierra.', 15.00, 55.00), -- ID 9
(1, 'PROD-CAR012', 'Carapulcra', 'Guiso de papa seca.', 40.00, 28.00), -- ID 10
(1, 'PROD-CHI013', 'Chicharrón de Cerdo', 'Panceta frita.', 45.00, 30.00), -- ID 11
(1, 'PROD-EST015', 'Estofado de Pollo', 'Pollo guisado.', 50.00, 24.00), -- ID 12
(1, 'PROD-OLL016', 'Olluquito con charqui', 'Olluco con carne seca.', 35.00, 26.00), -- ID 13
(1, 'PROD-MAR017', 'Arroz con mariscos', 'Arroz con mariscos.', 40.00, 40.00), -- ID 14
(1, 'PROD-CHR018', 'Chiriuchu', 'Mezcla fría tradicional.', 15.00, 60.00), -- ID 15
(1, 'PROD-JUA019', 'Juane', 'Arroz envuelto en bijao.', 30.00, 22.00), -- ID 16
(1, 'PROD-TAC020', 'Tacacho con Cecina', 'Plátano con chicharrón.', 30.00, 25.00), -- ID 17
(1, 'PROD-CHF021', 'Arroz Chaufa', 'Arroz salteado wok.', 80.00, 22.00), -- ID 18
(1, 'PROD-ADB022', 'Adobo Arequipeño', 'Guiso de cerdo.', 25.00, 28.00), -- ID 19
(1, 'PROD-CAU024', 'Cau Cau', 'Guiso de mondongo.', 45.00, 20.00), -- ID 20
(2, 'POST-PIC001', 'Picarones', 'Anillos fritos de calabaza.', 80.00, 10.00), -- ID 21
(2, 'POST-SUS002', 'Suspiro a la Limeña', 'Crema suave de leche.', 40.00, 12.00), -- ID 22
(2, 'POST-MAZ003', 'Mazamorra Morada', 'Postre de maíz morado.', 60.00, 8.00), -- ID 23
(2, 'POST-ALE004', 'Arroz con Leche', 'Arroz dulce con leche.', 60.00, 8.00), -- ID 24
(2, 'POST-TUR005', 'Turrón de Doña Pepa', 'Masa con jarabe.', 30.00, 15.00), -- ID 25
(2, 'POST-CHO006', 'Chocotejas', 'Chocolates con manjar.', 100.00, 4.00), -- ID 26
(3, 'ENTR-CAU004', 'Causa Rellena', 'Puré de papa con pollo.', 50.00, 18.00), -- ID 27
(3, 'ENTR-ANT009', 'Anticuchos', 'Brochetas de corazón.', 70.00, 20.00), -- ID 28
(3, 'ENTR-PPA014', 'Papa a la Huancaína', 'Papas en crema de queso.', 65.00, 14.00), -- ID 29
(3, 'ENTR-SOL023', 'Solterito Arequipeño', 'Ensalada de habas.', 40.00, 15.00), -- ID 30
(4, 'BEB-CHI001', 'Chicha de Jora', 'Maíz fermentado.', 50.00, 10.00), -- ID 31
(4, 'BEB-CHM002', 'Chicha Morada', 'Bebida de maíz morado.', 120.00, 8.00), -- ID 32
(4, 'BEB-PIS003', 'Pisco Sour', 'Cóctel de pisco.', 60.00, 22.00), -- ID 33
(4, 'BEB-INC004', 'Inca Kola', 'Gaseosa dorada.', 150.00, 6.00), -- ID 34
(4, 'BEB-MAT005', 'Mate de Coca', 'Infusión de coca.', 80.00, 5.00), -- ID 35
(4, 'BEB-EMO006', 'Emoliente', 'Bebida de cebada.', 70.00, 5.00); -- ID 36

-- --- COMPRA DE INVENTARIO ---
INSERT INTO Compra (idProveedor, idUsuario, nroFacturaNota, totalCompra) VALUES (1, 2, 'FAC-9921', 300.00);
INSERT INTO DetalleCompra (idCompra, idProducto, cantidad, precioCosto) VALUES (1, 1, 10.00, 30.00);

-- --- CLIENTES ---
INSERT INTO Cliente (ciNit, razonSocial) VALUES 
('0', 'CLIENTE GENERAL'),          -- ID 1
('1234567', 'Maria Elena Rodriguez'),     -- ID 2
('8765432101', 'Corporación Textil S.A.'),-- ID 3
('9876543', 'Carlos Lopez Justiniano'),   -- ID 4
('4561230', 'Sonia Vargas Osinaga'),      -- ID 5
('6543210', 'Juan Carlos Perez'),                       -- ID 6
('5264567', 'Juan Pérez'),                 -- ID 7
('7876003012', 'Empresa ABC S.R.L.'),     -- ID 8
('4859621', 'Alejandro Viscarra Marín'),  -- ID 9
('1020304', 'Claudia Arce Justiniano'),   -- ID 10
('3344556', 'Fernando Torrico Terceros'), -- ID 11
('2050809', 'Patricia Benavides Vega'),   -- ID 12
('8844112201', 'Inversiones Gastronómicas del Sur'), -- ID 13
('7412589', 'Ricardo Gareca Naranjo'),    -- ID 14
('9632587', 'Luciana Salazar Flores'),     -- ID 15
('5566778811', 'Hotelería San José S.R.L.');-- ID 16

-- --- VENTAS 1 A 10 REESTRUCTURADAS CON FECHAS DISTRIBUIDAS ---

-- --- DÍA: 01/05/2026 ---
-- Venta 1: Juan Carlos Perez, Jhoselin, Mesa 1
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (1, 1, 1, 'MESA', '2026-05-01 12:30:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (1, 1, 2, 35.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (1, 2, 1, 38.00);
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (1, 1, 108.00);

-- --- DÍA: 04/05/2026 ---
-- Venta 2: Maria Elena Rodriguez, Elizabeth, Llevar
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (2, 2, NULL, 'LLEVAR', '2026-05-04 19:15:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (2, 3, 3, 28.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (2, 4, 2, 42.00);
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (2, 3, 168.00);

-- --- DÍA: 07/05/2026 ---
-- Venta 3: Empresa ABC, Jhoselin, Mesa 3
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (8, 1, 3, 'MESA', '2026-05-07 14:00:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 9, 2, 55.00);  -- Pachamanca
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 33, 4, 22.00); -- Pisco Sour
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 32, 1, 8.00);   -- Chicha Morada
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (3, 2, 206.00);

-- --- DÍA: 10/05/2026 ---
-- Venta 4: SIN NOMBRE, Elizabeth, Delivery
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (6, 2, NULL, 'DELIVERY', '2026-05-10 21:30:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (4, 6, 1, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (4, 34, 1, 6.00);   -- Inca Kola
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (4, 1, 31.00);

-- --- DÍA: 12/05/2026 ---
-- Venta 5: Alejandro Viscarra, Jhoselin, Llevar
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (9, 1, NULL, 'LLEVAR', '2026-05-12 13:00:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 2, 1.00, 38.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 27, 1.00, 18.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 34, 2.00, 6.00);

-- --- DÍA: 15/05/2026 ---
-- Venta 6: Claudia Arce, Elizabeth, Mesa 2
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (10, 2, 2, 'MESA', '2026-05-15 13:45:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 3, 1.00, 28.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 4, 1.00, 14.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 29, 1.00, 14.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 32, 1.00, 8.00);

-- --- DÍA: 18/05/2026 ---
-- Venta 7: Juan Pérez, Jhoselin, Delivery
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (11, 1, NULL, 'DELIVERY', '2026-05-18 20:20:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 6, 1.00, 25.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 21, 1.00, 10.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 34, 1.00, 6.00);

-- --- DÍA: 20/05/2026 ---
-- Venta 8: Inversiones Gastronómicas del Sur, Elizabeth, Mesa 3
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (13, 2, 3, 'MESA', '2026-05-20 14:10:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 14, 3.00, 40.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 33, 3.00, 22.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 22, 3.00, 12.00);

-- --- DÍA: 22/05/2026 ---
-- Venta 9: SIN NOMBRE, Jhoselin, Llevar
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (6, 1, NULL, 'LLEVAR', '2026-05-22 18:30:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (9, 28, 2.00, 20.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (9, 31, 2.00, 10.00);

-- --- DÍA: 24/05/2026 ---
-- Venta 10: Hotelería San José S.R.L., Elizabeth, Mesa 1
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (16, 2, 1, 'MESA', '2026-05-24 13:15:00');
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 18, 5.00, 22.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 11, 5.00, 30.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 32, 2.00, 8.00);
GO

-- 6. CONSULTAS DE PRUEBA
SELECT * FROM Sala;
SELECT * FROM Mesa;
SELECT * FROM Compra;
SELECT * FROM DetalleCompra;
SELECT * FROM MetodoPago;
SELECT * FROM PagoVenta;

EXEC paCategoriaListar '';
EXEC paProductoListar '';
EXEC paClienteListar '';
EXEC paEmpleadoListar '';
EXEC paUsuarioListar '';
EXEC paVentaListar '';
EXEC paProductoTopMasVendidos '';
GO

-- --- EXTENSIÓN DE VENTAS 11 A 20 CON FECHAS DISTRIBUIDAS ---

-- --- DÍA: 25/05/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (14, 1, 2, 'MESA', '2026-05-25 13:15:00'); -- ID Venta 11
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (11, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (11, 28, 1, 20.00); -- Anticuchos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (11, 33, 3, 22.00); -- Pisco Sour
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (11, 2, 162.00);

-- --- DÍA: 26/05/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (15, 2, NULL, 'DELIVERY', '2026-05-26 20:45:00'); -- ID Venta 12
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (12, 27, 2, 18.00); -- Causa Rellena
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (12, 22, 2, 12.00); -- Suspiro a la Limeña
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (12, 32, 1, 8.00);  -- Chicha Morada
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (12, 3, 68.00);

-- --- DÍA: 28/05/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (5, 1, 3, 'MESA', '2026-05-28 12:30:00'); -- ID Venta 13
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (13, 11, 2, 30.00); -- Chicharrón de Cerdo
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (13, 23, 2, 8.00);  -- Mazamorra Morada
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (13, 31, 2, 10.00); -- Chicha de Jora
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (13, 1, 96.00);

-- --- DÍA: 29/05/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (4, 2, NULL, 'LLEVAR', '2026-05-29 19:00:00'); -- ID Venta 14
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (14, 6, 2, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (14, 34, 2, 6.00);  -- Inca Kola
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (14, 1, 62.00);

-- --- DÍA: 30/05/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (10, 1, 1, 'MESA', '2026-05-30 14:15:00'); -- ID Venta 15
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (15, 1, 2, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (15, 32, 2, 8.00);  -- Chicha Morada
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (15, 21, 3, 10.00); -- Picarones
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (15, 3, 116.00);

-- --- DÍA: 01/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (11, 2, NULL, 'DELIVERY', '2026-06-01 21:00:00'); -- ID Venta 16
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (16, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (16, 34, 1, 6.00);  -- Inca Kola
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (16, 2, 82.00);

-- --- DÍA: 03/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (12, 1, 2, 'MESA', '2026-06-03 13:45:00'); -- ID Venta 17
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (17, 3, 2, 28.00);  -- Ají de Gallina
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (17, 24, 2, 8.00);  -- Arroz con Leche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (17, 35, 2, 5.00);  -- Mate de Coca
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (17, 1, 82.00);

-- --- DÍA: 04/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (3, 2, NULL, 'LLEVAR', '2026-06-04 12:00:00'); -- ID Venta 18
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (18, 2, 4, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (18, 18, 4, 22.00); -- Arroz Chaufa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (18, 32, 8, 8.00);  -- Chicha Morada
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (18, 2, 304.00);

-- --- DÍA: 06/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (9, 1, NULL, 'DELIVERY', '2026-06-06 19:30:00'); -- ID Venta 19
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (19, 6, 2, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (19, 26, 5, 4.00);  -- Chocotejas
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (19, 36, 2, 5.00);  -- Emoliente
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (19, 3, 80.00);

-- --- DÍA: 07/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (1, 2, 1, 'MESA', '2026-06-07 14:00:00'); -- ID Venta 20
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (20, 1, 1, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (20, 28, 2, 20.00); -- Anticuchos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (20, 33, 2, 22.00); -- Pisco Sour
INSERT INTO PagoVenta (idVenta, idMetodoPago, monto) VALUES (20, 1, 119.00);
GO
-- --- EXTENSIÓN MASIVA DE VENTAS (VENTAS 21 A 50) ---
-- DISTRIBUIDAS EN DIFERENTES DÍAS Y HORARIOS (JUNIO 2026)

-- --- DÍA: 02/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (2, 1, 1, 'MESA', '2026-06-02 13:15:00'); -- Venta 21
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (21, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (21, 32, 2, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (4, 2, NULL, 'DELIVERY', '2026-06-02 20:30:00'); -- Venta 22
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (22, 6, 1, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (22, 21, 2, 10.00); -- Picarones
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (22, 34, 1, 6.00);  -- Inca Kola

-- --- DÍA: 05/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (5, 1, 3, 'MESA', '2026-06-05 12:45:00'); -- Venta 23
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (23, 1, 2, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (23, 33, 2, 22.00); -- Pisco Sour

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (6, 2, NULL, 'LLEVAR', '2026-06-05 19:15:00'); -- Venta 24
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (24, 11, 2, 30.00); -- Chicharrón de Cerdo
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (24, 31, 3, 10.00); -- Chicha de Jora

-- --- DÍA: 08/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (7, 1, 2, 'MESA', '2026-06-08 14:00:00'); -- Venta 25
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (25, 3, 2, 28.00);  -- Ají de Gallina
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (25, 23, 2, 8.00);  -- Mazamorra Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (9, 2, NULL, 'DELIVERY', '2026-06-08 21:00:00'); -- Venta 26
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (26, 2, 1, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (26, 34, 2, 6.00);  -- Inca Kola

-- --- DÍA: 10/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (10, 1, 1, 'MESA', '2026-06-10 13:30:00'); -- Venta 27
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (27, 8, 2, 32.00);  -- Seco de Carne
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (27, 32, 2, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (11, 2, NULL, 'LLEVAR', '2026-06-10 18:45:00'); -- Venta 28
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (28, 28, 3, 20.00); -- Anticuchos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (28, 24, 2, 8.00);  -- Arroz con Leche

-- --- DÍA: 12/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (12, 1, 3, 'MESA', '2026-06-12 12:15:00'); -- Venta 29
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (29, 14, 2, 40.00); -- Arroz con mariscos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (29, 33, 4, 22.00); -- Pisco Sour

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (13, 2, NULL, 'DELIVERY', '2026-06-12 20:00:00'); -- Venta 30
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (30, 6, 2, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (30, 22, 2, 12.00); -- Suspiro a la Limeña

-- --- DÍA: 14/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (14, 1, 2, 'MESA', '2026-06-14 13:00:00'); -- Venta 31
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (31, 2, 3, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (31, 32, 3, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (15, 2, NULL, 'LLEVAR', '2026-06-14 21:15:00'); -- Venta 32
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (32, 27, 2, 18.00); -- Causa Rellena
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (32, 36, 2, 5.00);  -- Emoliente

-- --- DÍA: 15/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (16, 1, 1, 'MESA', '2026-06-15 14:30:00'); -- Venta 33
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (33, 1, 3, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (33, 33, 2, 22.00); -- Pisco Sour

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (1, 2, NULL, 'DELIVERY', '2026-06-15 19:45:00'); -- Venta 34
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (34, 18, 2, 22.00); -- Arroz Chaufa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (34, 34, 2, 6.00);  -- Inca Kola

-- --- DÍA: 16/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (3, 1, 3, 'MESA', '2026-06-16 13:10:00'); -- Venta 35
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (35, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (35, 29, 2, 14.00); -- Papa a la Huancaína

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (5, 2, NULL, 'LLEVAR', '2026-06-16 20:20:00'); -- Venta 36
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (36, 11, 2, 30.00); -- Chicharrón de Cerdo
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (36, 31, 2, 10.00); -- Chicha de Jora

-- --- DÍA: 17/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (6, 1, 2, 'MESA', '2026-06-17 12:50:00'); -- Venta 37
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (37, 4, 2, 42.00);  -- Arroz con Pato
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (37, 32, 2, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (8, 2, NULL, 'DELIVERY', '2026-06-17 19:30:00'); -- Venta 38
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (38, 6, 3, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (38, 34, 3, 6.00);  -- Inca Kola

-- --- DÍA: 18/06/2026 ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (10, 1, 1, 'MESA', '2026-06-18 13:40:00'); -- Venta 39
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (39, 1, 2, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (39, 21, 4, 10.00); -- Picarones

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (12, 2, NULL, 'LLEVAR', '2026-06-18 20:45:00'); -- Venta 40
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (40, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (40, 33, 2, 22.00); -- Pisco Sour

-- --- DÍA: 19/06/2026 (Ayer) ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (14, 1, 3, 'MESA', '2026-06-19 13:00:00'); -- Venta 41
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (41, 14, 2, 40.00); -- Arroz con mariscos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (41, 32, 4, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (15, 2, NULL, 'DELIVERY', '2026-06-19 19:10:00'); -- Venta 42
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (42, 6, 2, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (42, 25, 2, 15.00); -- Turrón de Doña Pepa

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (2, 1, 2, 'MESA', '2026-06-19 21:30:00'); -- Venta 43
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (43, 28, 4, 20.00); -- Anticuchos
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (43, 33, 4, 22.00); -- Pisco Sour

-- --- DÍA: 20/06/2026 (Hoy) ---
INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (4, 2, 1, 'MESA', '2026-06-20 12:30:00'); -- Venta 44
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (44, 2, 2, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (44, 34, 2, 6.00);  -- Inca Kola

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (7, 1, NULL, 'LLEVAR', '2026-06-20 13:20:00'); -- Venta 45
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (45, 1, 2, 35.00);  -- Ceviche
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (45, 32, 2, 8.00);  -- Chicha Morada

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (9, 2, NULL, 'DELIVERY', '2026-06-20 14:00:00'); -- Venta 46
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (46, 18, 3, 22.00); -- Arroz Chaufa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (46, 34, 3, 6.00);  -- Inca Kola

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (11, 1, 2, 'MESA', '2026-06-20 19:30:00'); -- Venta 47
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (47, 3, 2, 28.00);  -- Ají de Gallina
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (47, 22, 2, 12.00); -- Suspiro a la Limeña

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (13, 2, 3, 'MESA', '2026-06-20 20:15:00'); -- Venta 48
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (48, 11, 2, 30.00); -- Chicharrón de Cerdo
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (48, 31, 4, 10.00); -- Chicha de Jora

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (16, 1, NULL, 'DELIVERY', '2026-06-20 21:00:00'); -- Venta 49
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (49, 6, 2, 25.00);  -- Pollo a la Brasa
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (49, 34, 2, 6.00);  -- Inca Kola

INSERT INTO Venta (idCliente, idUsuario, idMesa, tipoPedido, fechaRegistro) VALUES (1, 2, NULL, 'LLEVAR', '2026-06-20 21:45:00'); -- Venta 50
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (50, 2, 1, 38.00);  -- Lomo Saltado
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (50, 26, 3, 4.00);  -- Chocotejas
GO