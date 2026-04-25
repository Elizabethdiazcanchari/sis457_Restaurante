##  Modelo de Datos - Restaurante

### 1. Entidades de Catálogo
* **Categoria:** `id` (PK), `nombre`
* **Producto:** `id` (PK), `idCategoria` (FK), `codigo`, `nombre`, `descripcion`, `precioVenta`
* **Cliente:** `id` (PK), `nit`, `razonSocial`

### 2. Entidades de Personal y Acceso
* **Empleado:** `id` (PK), `cedulaIdentidad`, `nombres`, `primerApellido`, `segundoApellido`, `fechaNacimiento`, `direccion`, `celular`, `cargo`
* **Usuario:** `id` (PK), `idEmpleado` (FK), `usuario`, `clave`, `rol`

### 3. Entidades de Transacción (Venta)
* **Venta:** `id` (PK), `idCliente` (FK), `idEmpleado` (FK), `transaccion`, `fecha`, `total`
* **DetalleVenta:** `id` (PK), `idVenta` (FK), `idProducto` (FK), `cantidad`, `precioUnitario`, `total`

### 4. Campos de Auditoría y Control (Presentes en todas las tablas)
* `usuarioRegistro`: Usuario del sistema que creó el registro.
* `fechaRegistro`: Fecha y hora de creación.
* `estado`: Control de eliminación lógica (-1: Eliminado, 0: Inactivo, 1: Activo).
