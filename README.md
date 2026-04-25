##  Modelo de Datos- Restaurant

### 1. Entidades de Catálogo
* **Categoria:** uid=197609(Nam) gid=197121 groups=197121 (PK), 
* **Producto:** uid=197609(Nam) gid=197121 groups=197121 (PK),  (FK), , , , 
* **Cliente:** uid=197609(Nam) gid=197121 groups=197121 (PK), , 

### 2. Entidades de Personal y Acceso
* **Empleado:** uid=197609(Nam) gid=197121 groups=197121 (PK), , , , , , , , 
* **Usuario:** uid=197609(Nam) gid=197121 groups=197121 (PK),  (FK), , , 

### 3. Entidades de Transacción (Venta)
* **Venta:** uid=197609(Nam) gid=197121 groups=197121 (PK),  (FK),  (FK), , , 
* **DetalleVenta:** uid=197609(Nam) gid=197121 groups=197121 (PK),  (FK),  (FK), , , 

### 4. Campos de Auditoría y Control (Presentes en todas las tablas)
* : Usuario del sistema que creó el registro.
* : Fecha y hora de creación.
* : Control de eliminación lógica (-1: Eliminado, 0: Inactivo, 1: Activo).
