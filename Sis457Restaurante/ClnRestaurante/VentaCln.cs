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
            if (detalles.Count == 0) throw new InvalidOperationException("No se puede registrar una venta sin detalles.");

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

                        // Validar campos obligatorios de la infraestructura de tu DB para Venta
                        if (string.IsNullOrWhiteSpace(venta.tipoPedido))
                            venta.tipoPedido = "LLEVAR"; // Por defecto si se procesa directo en caja rápida

                        // 2) Insertar la venta (Cabecera)
                        context.Venta.Add(venta);
                        context.SaveChanges(); // Genera el ID de la venta (venta.id)

                        decimal totalVentaCalulado = 0;

                        // 3) Agregar detalles y validar/actualizar stock por código
                        foreach (var det in detalles)
                        {
                            det.idVenta = venta.id;
                            det.usuarioRegistro = string.IsNullOrWhiteSpace(det.usuarioRegistro) ? venta.usuarioRegistro : det.usuarioRegistro;
                            if (det.fechaRegistro == default(DateTime)) det.fechaRegistro = DateTime.Now;
                            if (det.estado == 0) det.estado = 1;

                            var producto = context.Producto.Find(det.idProducto);
                            if (producto == null)
                                throw new InvalidOperationException($"Producto con id {det.idProducto} no encontrado.");

                            // Validación estricta de Stock
                            if (producto.stock < det.cantidad)
                                throw new InvalidOperationException($"Stock insuficiente para el producto '{producto.nombre}'. Disponible: {producto.stock}, Solicitado: {det.cantidad}");

                            // CONTROL DE STOCK POR C# (Quitar esta línea si decides usar el Trigger AFTER INSERT en SQL Server)
                            producto.stock -= det.cantidad;

                            // Acumulamos el total para el módulo de pagos (cantidad * precioUnitario)
                            totalVentaCalulado += det.cantidad * det.precioUnitario;

                            context.DetalleVenta.Add(det);
                        }

                        // 4) REGISTRO OBLIGATORIO DEL PAGO (Módulo 3.6 de tu DB)
                        // Como especificas que por ahora solo se paga en efectivo (ID 1)
                        var pagoEfectivo = new PagoVenta
                        {
                            idVenta = venta.id,
                            idMetodoPago = 1, // 1 = Efectivo según tu requerimiento
                            monto = totalVentaCalulado,
                            usuarioRegistro = venta.usuarioRegistro,
                            fechaRegistro = DateTime.Now,
                            estado = 1
                        };
                        context.PagoVenta.Add(pagoEfectivo);

                        // Guardamos todos los cambios de detalles y del pago
                        context.SaveChanges();

                        trx.Commit(); // Consolida la transacción de forma segura
                        return venta.id;
                    }
                    catch
                    {
                        trx.Rollback(); // Si algo falla, deshace la venta, el stock, el pago y el cliente nuevo
                        throw;
                    }
                }
            }
        }

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
