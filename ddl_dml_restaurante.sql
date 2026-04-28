-- 1. CREACIÓN DE LA BASE DE DATOS Y LOGIN
CREATE DATABASE LabRestaurante;
GO
USE master;
GO
-- Crear el login para la aplicación
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'usrrestaurante')
BEGIN
    CREATE LOGIN usrrestaurante WITH PASSWORD = '123456',
      DEFAULT_DATABASE = LabRestaurante,
      CHECK_EXPIRATION = OFF,
      CHECK_POLICY = ON;
END
GO

USE LabRestaurante;
GO
CREATE USER usrrestaurante FOR LOGIN usrrestaurante;
GO
ALTER ROLE db_owner ADD MEMBER usrrestaurante;
GO

-- 2. ELIMINACIÓN DE TABLAS EN ORDEN DE JERARQUÍA
DROP TABLE IF EXISTS DetalleVenta;
DROP TABLE IF EXISTS Venta;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Empleado;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Categoria;

-- 3. CREACIÓN DE TABLAS BASE
CREATE TABLE Categoria (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(30) NOT NULL
);

CREATE TABLE Producto (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idCategoria INT NOT NULL,
  codigo VARCHAR(20) NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  descripcion VARCHAR(250) NULL,
  precioVenta DECIMAL(10,2) NOT NULL CHECK (precioVenta > 0),
  CONSTRAINT fk_Producto_Categoria FOREIGN KEY (idCategoria) REFERENCES Categoria(id)
);

CREATE TABLE Cliente (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  nitId VARCHAR(20) NOT NULL UNIQUE,
  razonSocial VARCHAR(100) NOT NULL
);

CREATE TABLE Empleado (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  cedulaIdentidad VARCHAR(12) NOT NULL UNIQUE,
  nombres VARCHAR(50) NOT NULL,
  primerApellido VARCHAR(50) NULL,
  segundoApellido VARCHAR(50) NULL,
  fechaNacimiento DATE NOT NULL,
  direccion VARCHAR(250) NOT NULL,
  celular BIGINT NOT NULL,
  cargo VARCHAR(50) NOT NULL -- Ej: Mesero, Cocinero, Administrador
);

CREATE TABLE Usuario (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idEmpleado INT NOT NULL,
  usuario VARCHAR(20) NOT NULL UNIQUE,
  clave VARCHAR(250) NOT NULL, -- Aquí guardarás el hash de la contraseña
  CONSTRAINT fk_Usuario_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
);

CREATE TABLE Venta (
  id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idCliente INT NOT NULL,
  idEmpleado INT NOT NULL,
  transaccion INT NOT NULL, -- Nro de factura o ticket
  fecha DATE NOT NULL DEFAULT GETDATE(),
  total DECIMAL(10,2) NOT NULL CHECK (total >= 0),
  CONSTRAINT fk_Venta_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(id),
  CONSTRAINT fk_Venta_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
);

CREATE TABLE DetalleVenta (
  id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
  idVenta BIGINT NOT NULL,
  idProducto INT NOT NULL,
  cantidad DECIMAL(10,2) NOT NULL CHECK (cantidad > 0),
  precioUnitario DECIMAL(10,2) NOT NULL CHECK (precioUnitario > 0),
  total DECIMAL(10,2) NOT NULL,
  CONSTRAINT fk_DetalleVenta_Venta FOREIGN KEY (idVenta) REFERENCES Venta(id),
  CONSTRAINT fk_DetalleVenta_Producto FOREIGN KEY (idProducto) REFERENCES Producto(id)
);

-- 4. ADICIÓN DE CAMPOS DE AUDITORÍA Y ELIMINACIÓN LÓGICA
-- Usamos un pequeño truco de SQL para aplicar a todas las tablas
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(name) + ' ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();' +
               'ALTER TABLE ' + QUOTENAME(name) + ' ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();' +
               'ALTER TABLE ' + QUOTENAME(name) + ' ADD estado INT NOT NULL DEFAULT 1;' 
FROM sys.tables;
EXEC sp_executesql @sql;
GO

-- 5. PROCEDIMIENTO ALMACENADO PARA LISTAR PRODUCTOS (Para tu CP)
CREATE PROC paProductoListar @parametro VARCHAR(50)
AS
  SELECT p.id, p.idCategoria, c.nombre AS categoria, p.codigo, p.nombre, p.descripcion, 
         p.precioVenta, p.estado
  FROM Producto p
  INNER JOIN Categoria c ON c.id = p.idCategoria
  WHERE p.estado = 1 AND (p.nombre + p.codigo + c.nombre) LIKE '%' + REPLACE(@parametro,' ','%') + '%'
  ORDER BY p.nombre;
GO


SELECT * FROM Categoria;
SELECT * FROM Producto;
