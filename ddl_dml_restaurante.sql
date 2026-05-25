-- 1. CREAR BASE DE DATOS Y LOGIN
USE master;
GO

DROP DATABASE IF EXISTS LabRestaurante;
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

-- 3. TABLAS PRINCIPALES

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

-- 4. TABLAS DE VENTAS

CREATE TABLE Venta (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    idUsuario INT NOT NULL,
    numeroTransaccion AS ('VEN-' + CAST(id AS VARCHAR(10))),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT fk_Venta_Cliente FOREIGN KEY (idCliente) REFERENCES Cliente(id),
    CONSTRAINT fk_Venta_Usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id)
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

-- 5. PROCEDIMIENTOS ALMACENADOS

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

CREATE PROC paVentaListar @parametro VARCHAR(100)
AS
    SELECT v.id, v.numeroTransaccion,
           c.razonSocial AS cliente,
           u.usuario AS Usuario,
           v.usuarioRegistro, v.fechaRegistro, v.estado
    FROM Venta v
    INNER JOIN Cliente c ON c.id = v.idCliente
    INNER JOIN Usuario u ON u.id = v.idUsuario
    WHERE v.estado <> -1
      AND (c.razonSocial + u.usuario + v.numeroTransaccion)
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

-- 6. DATOS DE PRUEBA (OPTIMIZADO Y AUMENTADO)

-- --- CATEGORÍAS ---
INSERT INTO Categoria (nombre) VALUES ('Platos Fuertes');             -- ID 1
INSERT INTO Categoria (nombre) VALUES ('Postres');                    -- ID 2
INSERT INTO Categoria (nombre) VALUES ('Acompañamientos y Entradas'); -- ID 3
INSERT INTO Categoria (nombre) VALUES ('Bebidas');                    -- ID 4
GO

-- --- PRODUCTOS ---

-- CATEGORÍA: PLATOS FUERTES (ID 1)
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, stock, precioVenta) VALUES
(1, 'PROD-CEV001', 'Ceviche', 'Pescado marinado en jugo de limón, ají picante y sal. Servido con lechuga, cebolla, maíz y cochayuyo.', 50.00, 35.00),
(1, 'PROD-LOM002', 'Lomo Saltado', 'Fusión peruano-china. Filete de carne, cebollas, tomates y papas fritas salteadas al wok. Con arroz.', 50.00, 38.00),
(1, 'PROD-AJI003', 'Ají de Gallina', 'Pollo deshilachado en crema de ají amarillo, nueces, queso y leche. Servido con papas y arroz.', 50.00, 28.00),
(1, 'PROD-ARR005', 'Arroz con Pato', 'Pato marinado en cerveza negra y especias, cocinado lentamente con arroz al culantro. Estilo norteño.', 30.00, 42.00),
(1, 'PROD-CUY006', 'Cuy al Horno', 'Plato tradicional andino. Cuy cocinado en horno de leña, acompañado de papas y tallarines.', 20.00, 65.00),
(1, 'PROD-POL007', 'Pollo a la Brasa', 'Pollo marinado (soya, ajo, comino) cocinado a las brasas. Servido con papas fritas y ensalada.', 100.00, 25.00),
(1, 'PROD-ROC008', 'Rocoto Relleno', 'Plato arequipeño. Rocoto picante relleno de carne salteada y verduras, cubierto con queso derretido.', 40.00, 30.00),
(1, 'PROD-SEC010', 'Seco de Carne', 'Guiso de carne con chicha de jora y cilantro fresco. Acompañado de frijoles y arroz blanco.', 50.00, 32.00),
(1, 'PROD-PAC011', 'Pachamanca', 'Carnes y verduras marinadas con huacatay, cocinadas bajo tierra con piedras calientes.', 15.00, 55.00),
(1, 'PROD-CAR012', 'Carapulcra', 'Guiso afroperuano de papa seca, carne de cerdo y pollo, pimientos, clavo de olor y ajo.', 40.00, 28.00),
(1, 'PROD-CHI013', 'Chicharrón de Cerdo', 'Panceta de cerdo frita en su propia grasa. Servido con papas fritas, choclo y salsa criolla.', 45.00, 30.00),
(1, 'PROD-EST015', 'Estofado de Pollo', 'Pollo guisado con zanahorias, arvejas y papas en salsa de ají panca, tomate y vino tinto.', 50.00, 24.00),
(1, 'PROD-OLL016', 'Olluquito con charqui', 'Olluco picado con charqui (carne seca de alpaca o llama) sazonado con ají amarillo.', 35.00, 26.00),
(1, 'PROD-MAR017', 'Arroz con mariscos', 'Arroz sazonado y cocinado con mariscos selectos, guisantes, zanahoria y un toque de queso parmesano.', 40.00, 40.00),
(1, 'PROD-CHR018', 'Chiriuchu', 'Plato bandera de Cusco. Mezcla fría de algas, huevera, gallina, charqui, cuy, morcilla y maíz.', 15.00, 60.00),
(1, 'PROD-JUA019', 'Juane', 'Plato amazónico de arroz, pollo y especias envuelto en hojas de bijao y cocinado al vapor.', 30.00, 22.00),
(1, 'PROD-TAC020', 'Tacacho con Cecina', 'Plátano verde frito y machacado con chicharrón, servido típicamente en forma de esferas.', 30.00, 25.00),
(1, 'PROD-CHF021', 'Arroz Chaufa', 'Fusión Chifa. Arroz salteado a fuego alto con cebolla china, jengibre, sillao y trozos de carne.', 80.00, 22.00),
(1, 'PROD-ADB022', 'Adobo Arequipeño', 'Guiso dominical picante de cerdo marinado en ají panca y chicha de jora.', 25.00, 28.00),
(1, 'PROD-CAU024', 'Cau Cau', 'Guiso tradicional de mondongo (o pollo) y papas en cuadraditos con palillo, ají amarillo y menta.', 45.00, 20.00),
(1, 'PROD-APO025', 'Arroz con Pollo', 'Pollo cocinado con arroz sazonado con cilantro (culantro), ajo, pimientos y alverjas.', 60.00, 22.00),
(1, 'PROD-TAR026', 'Guiso de Tarwi', 'Superalimento andino. Tarwi mezclado con queso, leche, mantequilla y ajo molido.', 25.00, 24.00);

-- CATEGORÍA: POSTRES (ID 2)
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, stock, precioVenta) VALUES
(2, 'POST-PIC001', 'Picarones', 'Anillos fritos de masa de calabaza y camote, bañados en dulce miel de chancaca.', 80.00, 10.00),
(2, 'POST-SUS002', 'Suspiro a la Limeña', 'Crema suave de leche condensada y evaporada, coronada con merengue al oporto y canela.', 40.00, 12.00),
(2, 'POST-MAZ003', 'Mazamorra Morada', 'Postre gelatinoso de maíz morado cocinado con piña, ciruelas y espesado con chuño.', 60.00, 8.00),
(2, 'POST-ALE004', 'Arroz con Leche', 'Arroz cocinado en leche aromatizada con canela y cáscara de limón, endulzado al punto.', 60.00, 8.00),
(2, 'POST-TUR005', 'Turrón de Doña Pepa', 'Masa horneada con aroma a anís, dispuesta en capas con jarabe de frutas y grageas de colores.', 30.00, 15.00),
(2, 'POST-CHO006', 'Chocotejas', 'Dulces de Ica rellenos de manjar blanco y frutos secos (pecanas/guindones), cubiertos de chocolate.', 100.00, 4.00);

-- CATEGORÍA: ACOMPAÑAMIENTOS Y ENTRADAS (ID 3)
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, stock, precioVenta) VALUES
(3, 'ENTR-CAU004', 'Causa Rellena', 'Puré de papa amarilla con limón y ají amarillo, relleno de pollo, atún o mariscos con palta.', 50.00, 18.00),
(3, 'ENTR-ANT009', 'Anticuchos', 'Brochetas de corazón de ternera marinado en ají panca, ajo y comino, cocinadas a la parrilla.', 70.00, 20.00),
(3, 'ENTR-PPA014', 'Papa a la Huancaína', 'Papas cocidas bañadas en crema de queso fresco, ají amarillo y galletas. Adornado con huevo y aceituna.', 65.00, 14.00),
(3, 'ENTR-SOL023', 'Solterito Arequipeño', 'Ensalada fresca de habas, maíz (choclo), tomate, cebolla, aceitunas negras y queso fresco.', 40.00, 15.00);

-- CATEGORÍA: BEBIDAS (ID 4)
INSERT INTO Producto (idCategoria, codigo, nombre, descripcion, stock, precioVenta) VALUES
(4, 'BEB-CHI001', 'Chicha de Jora', 'Bebida milenaria andina elaborada a base de maíz fermentado, usada tradicionalmente en ceremonias y guisos.', 50.00, 10.00),
(4, 'BEB-CHM002', 'Chicha Morada', 'Bebida refrescante tradicional preparada a base de maíz morado hervido con piña, manzana, canela, clavo de olor y limón.', 120.00, 8.00),
(4, 'BEB-PIS003', 'Pisco Sour', 'El cóctel bandera del Perú. Elaborado a base de pisco, jugo de limón, jarabe de goma, clara de huevo y unas gotas de amargo de angostura.', 60.00, 22.00),
(4, 'BEB-INC004', 'Inca Kola', 'La gaseosa más popular del Perú, de color dorado y sabor dulce único, ideal para acompañar el Chifa y otros platos criollos.', 150.00, 6.00),
(4, 'BEB-MAT005', 'Mate de Coca', 'Infusión tradicional andina elaborada con hojas de coca naturales, muy conocida por sus propiedades digestivas y energizantes.', 80.00, 5.00),
(4, 'BEB-EMO006', 'Emoliente', 'Bebida medicinal y reconfortante que se sirve caliente o fría, preparada a base de cebada tostada, linaza, alfalfa y jugo de limón.', 70.00, 5.00);
GO

-- --- EMPLEADOS ---
INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('1234567', 'Jhoselin', 'Figueroa', 'Colque', '1990-05-15', 'Av. 6 de Agosto 123', 71234567, 'Mesero');

INSERT INTO Empleado (cedulaIdentidad, nombres, primerApellido, segundoApellido, fechaNacimiento, direccion, celular, cargo)
VALUES ('7654321', 'Elizabeth', 'Diaz', 'Canchari', '1988-03-20', 'Calle Potosí 456', 76543210, 'Administrador');
GO

-- --- USUARIOS ---
INSERT INTO Usuario (idEmpleado, usuario, clave) VALUES (1, 'jhoselin', 'I0HCOO/NSSY6WOS9POP5XW==');
INSERT INTO Usuario (idEmpleado, usuario, clave) VALUES (2, 'elizabet', 'I0HCOO/NSSY6WOS9POP5XW==');
GO

-- --- CLIENTES ---
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('6543210', 'Juan Carlos Perez');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('1234567', 'Maria Elena Rodriguez');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('8765432101', 'Corporación Textil S.A.');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('9876543', 'Carlos Lopez Justiniano');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('4561230', 'Sonia Vargas Osinaga');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('0', 'SIN NOMBRE');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('5264567', 'Juan Pérez');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('7876003012', 'Empresa ABC S.R.L.');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('4859621', 'Alejandro Viscarra Marín');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('1020304', 'Claudia Arce Justiniano');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('3344556', 'Fernando Torrico Terceros');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('2050809', 'Patricia Benavides Vega');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('8844112201', 'Inversiones Gastronómicas del Sur');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('7412589', 'Ricardo Gareca Naranjo');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('9632587', 'Luciana Salazar Flores');
INSERT INTO Cliente (ciNit, razonSocial) VALUES ('5566778811', 'Hotelería San José S.R.L.');
GO

-- --- VENTAS ---
-- Venta 1: Registrada por Jhoselin (Usuario 1) para Juan Carlos Perez (Cliente 1)
INSERT INTO Venta (idCliente, idUsuario) VALUES (1, 1);
-- Venta 2: Registrada por Elizabeth (Usuario 2) para Maria Elena Rodriguez (Cliente 2)
INSERT INTO Venta (idCliente, idUsuario) VALUES (2, 2);
-- Venta 3 (Aumentada): Registrada por Jhoselin (Usuario 1) para Empresa ABC S.R.L. (Cliente 8)
INSERT INTO Venta (idCliente, idUsuario) VALUES (8, 1);
-- Venta 4 (Aumentada): Registrada por Elizabeth (Usuario 2) para el cliente rápido SIN NOMBRE (Cliente 6)
INSERT INTO Venta (idCliente, idUsuario) VALUES (6, 2);

-- --- DETALLES DE VENTA ---
-- Detalles de la Venta 1 (2 Ceviches y 1 Lomo Saltado)
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (1, 1, 2, 35.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (1, 2, 1, 38.00);

-- Detalles de la Venta 2 (3 Ajíes de Gallina y 2 Arroz con Pato)
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (2, 3, 3, 28.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (2, 4, 2, 42.00);

-- Detalles de la Venta 3 (Mesa corporativa: 2 Pachamancas, 1 Ronda de Pisco Sour, 1 Chicha Morada)
-- IDs de productos correspondientes en orden de inserción: Pachamanca (ID 9), Pisco Sour (ID 33), Chicha Morada (ID 32)
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 9, 2, 55.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 33, 4, 22.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (3, 32, 1, 8.00);

-- Detalles de la Venta 4 (Venta de mostrador rápida: 1 Pollo a la Brasa, 1 Inca Kola)
-- IDs de productos correspondientes: Pollo a la brasa (ID 6), Inca Kola (ID 34)
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (4, 6, 1, 25.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (4, 34, 1, 6.00);
GO

-- --- VENTA 5: Atendida por Jhoselin (Usuario 1) para Alejandro Viscarra (Cliente 9)
-- Pedido: 1 Lomo Saltado (ID 2), 1 Causa Rellena (ID 27), 2 Inca Kolas (ID 34)
INSERT INTO Venta (idCliente, idUsuario) VALUES (9, 1);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 2, 1.00, 38.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 27, 1.00, 18.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (5, 34, 2.00, 6.00);

-- --- VENTA 6: Atendida por Elizabeth (Usuario 2) para Claudia Arce (Cliente 10)
-- Pedido: 1 Ají de Gallina (ID 3), 1 Papa a la Huancaína (ID 29), 1 Chicha Morada (ID 32)
INSERT INTO Venta (idCliente, idUsuario) VALUES (10, 2);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 3, 1.00, 28.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 29, 1.00, 14.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (6, 32, 1.00, 8.00);

-- --- VENTA 7: Atendida por Jhoselin (Usuario 1) para Fernando Torrico (Cliente 11)
-- Pedido: 1 Pollo a la Brasa (ID 6), 1 Porción de Picarones (ID 21), 1 Inca Kola (ID 34)
INSERT INTO Venta (idCliente, idUsuario) VALUES (11, 1);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 6, 1.00, 25.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 21, 1.00, 10.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (7, 34, 1.00, 6.00);

-- --- VENTA 8: Atendida por Elizabeth (Usuario 2) para Inversiones Gastronómicas (Cliente 13)
-- Pedido de Negocios: 3 Arroz con Mariscos (ID 14), 3 Pisco Sours (ID 33), 3 Suspiros a la Limeña (ID 22)
INSERT INTO Venta (idCliente, idUsuario) VALUES (13, 2);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 14, 3.00, 40.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 33, 3.00, 22.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (8, 22, 3.00, 12.00);

-- --- VENTA 9: Atendida por Jhoselin (Usuario 1) para el cliente rápido "SIN NOMBRE" (Cliente 6)
-- Pedido al paso: 2 Anticuchos (ID 28), 2 Chichas de Jora (ID 31)
INSERT INTO Venta (idCliente, idUsuario) VALUES (6, 1);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (9, 28, 2.00, 20.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (9, 31, 2.00, 10.00);

-- --- VENTA 10: Atendida por Elizabeth (Usuario 2) para Hotelería San José (Cliente 16)
-- Evento corporativo: 5 Arroz Chaufa (ID 18), 5 Chicharrón de Cerdo (ID 11), 2 Jarras de Chicha Morada (ID 32)
INSERT INTO Venta (idCliente, idUsuario) VALUES (16, 2);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 18, 5.00, 22.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 11, 5.00, 30.00);
INSERT INTO DetalleVenta (idVenta, idProducto, cantidad, precioUnitario) VALUES (10, 32, 2.00, 8.00);
GO

-- 7. CONSULTAS DE PRUEBA

SELECT * FROM Categoria;
SELECT * FROM Producto;
SELECT * FROM Empleado;
SELECT * FROM Cliente;
SELECT * FROM Usuario;
SELECT * FROM Venta;
SELECT * FROM DetalleVenta;

EXEC paCategoriaListar '';
EXEC paProductoListar '';
EXEC paClienteListar '';
EXEC paEmpleadoListar '';
EXEC paUsuarioListar '';
EXEC paVentaListar '';