using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class ProductoCln
    {
        public static int crear(Producto producto)
        {
            using (var context = new LabRestauranteEntities())
            {
                context.Producto.Add(producto);
                context.SaveChanges();
                return producto.id;
            }
        }

        public static int modificar(Producto producto)
        {
            using (var context = new LabRestauranteEntities())
            {
                var existente = context.Producto.Find(producto.id);
                if (existente != null)
                {
                    existente.idCategoria = producto.idCategoria;
                    existente.codigo = producto.codigo;
                    existente.nombre = producto.nombre;
                    existente.descripcion = producto.descripcion;
                    existente.precioVenta = producto.precioVenta;
                    existente.usuarioRegistro = producto.usuarioRegistro;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabRestauranteEntities())
            {
                var existente = context.Producto.Find(id);
                if (existente != null)
                {
                    existente.estado = -1;
                    existente.usuarioRegistro = usuarioRegistro;
                    return context.SaveChanges();
                }
                return 0;
            }
        }

        public static Producto obtenerUno(int id)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Producto.Find(id);
            }
        }

        public static List<Producto> listar()
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Producto
                    .Where(x => x.estado == 1)
                    .OrderBy(x => x.descripcion)
                    .ToList();
            }
        }

        public static List<paProductoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.paProductoListar(parametro.Trim()).ToList();
            }
        }
    }
}
