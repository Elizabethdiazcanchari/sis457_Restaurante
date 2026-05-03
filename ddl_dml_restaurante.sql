-- ============================
-- 1. CREAR BASE DE DATOS Y LOGIN
-- ============================
USE master;
GO

DROP DATABASE IF EXISTS LabRestaurante;
GO

CREATE DATABASE LabRestaurante;
GO

DROP LOGIN IF EXISTS usrrestaurante;
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

-- ============================
-- 2. ELIMINAR OBJETOS EXISTENTES
-- ============================
DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Categoria;
DROP PROC IF EXISTS paCategoriaListar;
DROP PROC IF EXISTS paProductoListar;
DROP PROC IF EXISTS paClienteListar;
DROP PROC IF EXISTS paEmpleadoListar;
DROP PROC IF EXISTS paUsuarioListar;
DROP PROC IF EXISTS paVentaListar;
DROP PROC IF EXISTS paDetalleVentaListar;
GO

-- ============================
-- 3. TABLAS PRINCIPALES
-- ============================

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
    precioVenta DECIMAL(10,2) NOT NULL CHECK (precioVenta > 0),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Producto_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id)
);

CREATE TABLE Cliente (
    id INT PRIMARY KEY IDENTITY(1,1),
    nitId VARCHAR(20) NOT NULL UNIQUE,
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
    cargo VARCHAR(50) NOT NULL,
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

-- ============================
-- 4. TABLAS DE VENTAS
-- ============================

CREATE TABLE Venta (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    idEmpleado INT NOT NULL,
    numeroTransaccion AS ('VEN-' + CAST(id AS VARCHAR(10))),
    fecha DATE NOT NULL DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (total >= 0),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Venta_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(id),
    CONSTRAINT fk_Venta_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
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
GO

-- ============================
-- 5. PROCEDIMIENTOS ALMACENADOS
-- ============================

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
           p.nombre, p.descripcion, p.precioVenta, 
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
    SELECT c.id, c.nitId, c.razonSocial, 
           c.usuarioRegistro, c.fechaRegistro, c.estado
    FROM Cliente c
    WHERE c.estado <> -1
      AND (c.nitId + c.razonSocial) LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
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

CREATE PROC paVentaListar @parametro VARCHAR(100)
AS
    SELECT v.id, v.numeroTransaccion,
           c.razonSocial AS cliente,
           e.nombres + ' ' + ISNULL(e.primerApellido,'') AS empleado,
           v.fecha, v.total,
           v.usuarioRegistro, v.fechaRegistro, v.estado
    FROM Venta v
    INNER JOIN Cliente c ON c.id = v.idCliente
    INNER JOIN Empleado e ON e.id = v.idEmpleado
    WHERE v.estado <> -1
      AND (c.razonSocial + e.nombres + ISNULL(e.primerApellido,'') + v.numeroTransaccion)
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
    ORDER BY v.fechaRegistro DESC;
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

-- ============================
-- 6. DATOS DE PRUEBA
-- ============================

-- Categorías
INSERT INTO Categoria (nombre) VALUES ('Entradas');
INSERT INTO Categoria (nombre) VALUES ('Platos Principales');
INSERT INTO Categoria (nombre) VALUES ('Bebidas');
INSERT INTO Categoria (nombre) VALUES ('Postres');

-- Productos
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, precioVenta)
VALUES (1, 'EN-SO', 'Sopa del Día', 'Sopa casera según temporada', 15.00);

INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, precioVenta)
VALUES (2, 'PP-LO', 'Lomo Saltado', 'Lomo con verduras salteadas y arroz', 45.00);

INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, precioVenta)
VALUES (3, 'BE-JN', 'Jugo Natural', 'Jugo de fruta fresca 500ml', 10.00);

INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, precioVenta)
VALUES (4, 'PS-FL', 'Flan Casero', 'Flan de vainilla con caramelo', 12.00);

-- Empleados
INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('1234567', 'Carlos', 'Mamani', 'Quispe', '1990-05-15', 'Av. 6 de Agosto 123', 71234567, 'Mesero');

INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('7654321', 'Ana', 'Flores', 'Ramos', '1988-03-20', 'Calle Potosí 456', 76543210, 'Administrador');

-- Clientes
INSERT INTO Cliente (nitId, razonSocial) VALUES ('1234567', 'Juan Pérez');
INSERT INTO Cliente (nitId, razonSocial) VALUES ('9876543', 'Empresa ABC S.R.L.');

-- Usuarios
INSERT INTO Usuario (idEmpleado, usuario, clave)
VALUES (1, 'jhoselin', 'I0HCOO/NSSY6WOS9POP5XW==');

INSERT INTO Usuario (idEmpleado, usuario, clave)
VALUES (2, 'elizabet', 'I0HCOO/NSSY6WOS9POP5XW==');

-- ============================
-- 7. CONSULTAS DE PRUEBA
-- ============================
SELECT * FROM Categoria;
SELECT * FROM Producto;
SELECT * FROM Empleado;
SELECT * FROM Cliente;
SELECT * FROM Usuario;

EXEC paCategoriaListar '';
EXEC paProductoListar '';
EXEC paClienteListar '';
EXEC paEmpleadoListar '';
EXEC paUsuarioListar '';
EXEC paVentaListar '';