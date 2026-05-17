using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class VentaCln
    {
        public static long crearConDetallesYCliente(Venta venta, List<DetalleVenta> detalles, Cliente nuevoCliente = null)
        {
            if (venta == null) throw new ArgumentNullException(nameof(venta));
            if (detalles == null) throw new ArgumentNullException(nameof(detalles));
            using (var context = new LabRestauranteEntities())
            {
                using (var trx = context.Database.BeginTransaction())
                {
                    try
                    {
                        // 1) Si se envió un nuevo cliente, se registra primero
                        if (nuevoCliente != null)
                        {
                            // Verificación secundaria por seguridad (evitar duplicados de CI/NIT concurrentes)
                            var clienteExistente = context.Cliente.FirstOrDefault(c => c.ciNit == nuevoCliente.ciNit && c.estado != -1);
                            if (clienteExistente != null)
                            {
                                venta.idCliente = clienteExistente.id;
                            }
                            else
                            {
                                context.Cliente.Add(nuevoCliente);
                                context.SaveChanges(); // Genera el ID del cliente
                                venta.idCliente = nuevoCliente.id; // Asignamos el ID generado a la venta
                            }
                        }

                        // 2) Insertar la venta
                        context.Venta.Add(venta);
                        context.SaveChanges(); // Genera el ID de la venta

                        // 3) Agregar detalles y actualizar stock
                        foreach (var det in detalles)
                        {
                            det.idVenta = venta.id;
                            det.usuarioRegistro = string.IsNullOrWhiteSpace(det.usuarioRegistro) ? venta.usuarioRegistro : det.usuarioRegistro;
                            if (det.fechaRegistro == default(DateTime)) det.fechaRegistro = DateTime.Now;
                            if (det.estado == 0) det.estado = 1;

                            var producto = context.Producto.Find(det.idProducto);
                            if (producto == null)
                                throw new InvalidOperationException($"Producto con id {det.idProducto} no encontrado.");

                            if (producto.stock < det.cantidad)
                                throw new InvalidOperationException($"Stock insuficiente para el producto {producto.nombre} (id {producto.id}).");

                            producto.stock -= det.cantidad;
                            context.DetalleVenta.Add(det);
                        }

                        context.SaveChanges();
                        trx.Commit(); // Guarda todo en conjunto de forma segura
                        return venta.id;
                    }
                    catch
                    {
                        trx.Rollback(); // Si algo falla, deshace la venta, el stock y el cliente nuevo
                        throw;
                    }
                }
            }
        }

        // Se mantiene el método anterior por compatibilidad con otros formularios
        public static long crearConDetalles(Venta venta, List<DetalleVenta> detalles)
        {
            return crearConDetallesYCliente(venta, detalles, null);
        }

        public static List<Venta> listar()
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Venta.Where(x => x.estado == 1).ToList();
            }
        }

        public static List<paVentaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.paVentaListar(parametro).ToList();
            }
        }

        // NUEVO: obtener cabecera del pedido por id
        public static Venta obtenerUno(int id)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Venta.Find(id);
            }
        }
    }
}
