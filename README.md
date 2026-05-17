# Sistema de Gestión y Ventas - Restaurante de Comida Peruana (SIS457)

Este repositorio contiene el proyecto de laboratorio desarrollado para la materia **SIS457**, correspondiente a la carrera de Informatica de la **Universidad de San Francisco Xavier de Chuquisaca (USFX)**.

El sistema es una aplicación de escritorio desarrollada bajo una arquitectura de **3 Capas**, utilizando **C# (Windows Forms)** y **Entity Framework** con una base de datos en **SQL Server**.

---

## 1. Descripción del Negocio
El proyecto está enfocado en la digitalización y optimización de los procesos operativos de un **Restaurante de Comidas Peruanas**. El negocio requiere un control automatizado de su oferta gastronómica, administración del personal, registro de clientes y, de manera central, la gestión del flujo de caja mediante la emisión rápida de comprobantes.

El software permite:
*   Autenticar de manera segura a los usuarios del sistema según su personal asociado.
*   Categorizar y administrar el menú (platos, bebidas y postres) controlando precios y stock disponible.
*   Registrar las ventas en un esquema maestro-detalle calculando los montos de forma automatizada y transparente.

---

## 2. Estructura de la Solución (Convenciones Advanced)

La solución en Visual Studio se denomina `Sis457Restaurante` y está dividida estrictamente en los siguientes proyectos para garantizar una arquitectura limpia:

*   **`CadRestaurante` (Capa de Acceso a Datos):** Biblioteca de clases que aloja el mapeo de Entity Framework, el script DDL (`ddl_dml_restaurante.sql`) y los modelos de datos.
*   **`ClnRestaurante` (Capa Lógica de Negocios):** Biblioteca de clases que contiene las reglas de negocio, validaciones intermedias y llamadas a los métodos de la CAD.
*   **`CpRestaurante` (Capa de Presentación):** Aplicación de interfaz gráfica en Windows Forms encargada de la interacción con el usuario final y validación de campos vacíos.

---

## 3. Modelo de Datos

A continuación se detallan las entidades que estructuran la base de datos `LabRestaurante`, sincronizadas exactamente con las definiciones del script DDL.

### 3.1. Entidades de Catálogo

*   **Categoria:** Clasificación del menú (ej: Entradas, Segundos, Bebidas).
    *   `id` (INT, PK, IDENTITY)
    *   `nombre` (VARCHAR(50), NOT NULL, UNIQUE)
*   **Producto:** Platos y productos ofertados en el restaurante.
    *   `id` (INT, PK, IDENTITY)
    *   `idCategoria` (INT, FK -> Categoria.id)
    *   `codigo` (VARCHAR(20), NOT NULL)
    *   `nombre` (VARCHAR(100), NOT NULL)
    *   `descripcion` (VARCHAR(250), NULL)
    *   `imagenUrl` (VARCHAR(300), NULL)
    *   `stock` (DECIMAL(10,2), NOT NULL, DEFAULT 0, CHECK >= 0)
    *   `precioVenta` (DECIMAL(10,2), NOT NULL, CHECK > 0)
*   **Cliente:** Información de los comensales para la facturación.
    *   `id` (INT, PK, IDENTITY)
    *   `ciNit` (VARCHAR(20), NOT NULL, UNIQUE)
    *   `razonSocial` (VARCHAR(100), NOT NULL)

### 3.2. Entidades de Personal y Acceso

*   **Empleado:** Datos del personal contratado en el restaurante.
    *   `id` (INT, PK, IDENTITY)
    *   `cedulaIdentidad` (VARCHAR(12), NOT NULL, UNIQUE)
    *   `nombres` (VARCHAR(50), NOT NULL)
    *   `primerApellido` (VARCHAR(50), NULL)
    *   `segundoApellido` (VARCHAR(50), NULL)
    *   `fechaNacimiento` (DATE, NOT NULL)
    *   `direccion` (VARCHAR(250), NOT NULL)
    *   `celular` (BIGINT, NOT NULL)
    *   `cargo` (VARCHAR(50), NOT NULL)
*   **Usuario:** Credenciales de ingreso al sistema vinculadas a un empleado.
    *   `id` (INT, PK, IDENTITY)
    *   `idEmpleado` (INT, FK -> Empleado.id)
    *   `usuario` (VARCHAR(20), NOT NULL, UNIQUE)
    *   `clave` (VARCHAR(250), NOT NULL)

### 3.3. Entidades de Transacción (Venta)

*   **Venta:** Cabecera que registra los datos globales de cada consumo cobrado.
    *   `id` (BIGINT, PK, IDENTITY)
    *   `idCliente` (INT, FK -> Cliente.id)
    *   `idEmpleado` (INT, FK -> Empleado.id)
    *   `numeroTransaccion` (VARCHAR(14), Campo Calculado: `'VEN-' + id`)
*   **DetalleVenta:** Desglose individual de los platos y cantidades de una venta.
    *   `id` (BIGINT, PK, IDENTITY)
    *   `idVenta` (BIGINT, FK -> Venta.id)
    *   `idProducto` (INT, FK -> Producto.id)
    *   `cantidad` (DECIMAL(10,2), NOT NULL, CHECK > 0)
    *   `precioUnitario` (DECIMAL(10,2), NOT NULL, CHECK > 0)
    *   `total` (DECIMAL(21,4), Campo Calculado Persistido: `cantidad * precioUnitario`)

### 3.4. Campos de Auditoría y Control (Presentes en todas las tablas)

Para mantener la consistencia, el rastreo de datos y cumplir con el requisito de **eliminación lógica**, todas las tablas incorporan:
*   `usuarioRegistro` (VARCHAR(50), NOT NULL, DEFAULT SUSER_NAME()): Identifica el usuario de BD que registró la fila.
*   `fechaRegistro` (DATETIME, NOT NULL, DEFAULT GETDATE()): Almacena la marca de tiempo exacta de la creación.
*   `estado` (SMALLINT, NOT NULL, DEFAULT 1): Define la persistencia lógica del registro donde:
    *   `1`: Activo (Visible en el sistema)
    *   `0`: Inactivo
    *   `-1`: Eliminado (Borrado lógico requerido por la materia)

---

## 4. Integrantes del Grupo
*   **Integrante 1:** Elizabeth Diaz Canchari
*   **Integrante 2:** Jhoselin Figueroa Colque

**Docente:** Ing. Esnor Noel Enrique Vaca Moreno  
**Fecha de Exposición:** 01/06/2026