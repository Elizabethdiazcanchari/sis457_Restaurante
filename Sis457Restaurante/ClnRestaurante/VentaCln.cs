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
        public static long crearConDetalles(Venta venta, List<DetalleVenta> detalles)
        {
            if (venta == null) throw new ArgumentNullException(nameof(venta));
            if (detalles == null) throw new ArgumentNullException(nameof(detalles));
            using (var context = new LabRestauranteEntities())
            {
                using (var trx = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Insertar la venta (genera id)
                        context.Venta.Add(venta);
                        context.SaveChanges();

                        // Agregar detalles y actualizar stock
                        foreach (var det in detalles)
                        {
                            // Preparar detalle
                            det.idVenta = venta.id;
                            det.usuarioRegistro = string.IsNullOrWhiteSpace(det.usuarioRegistro) ? venta.usuarioRegistro : det.usuarioRegistro;
                            if (det.fechaRegistro == default(DateTime)) det.fechaRegistro = DateTime.Now;
                            if (det.estado == 0) det.estado = 1;

                            // Comprobar y actualizar producto
                            var producto = context.Producto.Find(det.idProducto);
                            if (producto == null)
                                throw new InvalidOperationException($"Producto con id {det.idProducto} no encontrado.");

                            if (producto.stock < det.cantidad)
                                throw new InvalidOperationException($"Stock insuficiente para el producto {producto.nombre} (id {producto.id}).");

                            producto.stock -= det.cantidad;

                            // Agregar detalle
                            context.DetalleVenta.Add(det);
                        }

                        context.SaveChanges();
                        trx.Commit();
                        return venta.id;
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<Venta> listar()
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Venta.Where(x => x.estado != -1).ToList();
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
