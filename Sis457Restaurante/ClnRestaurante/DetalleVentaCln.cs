using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class DetalleVentaCln
    {
        public static long crear(DetalleVenta detalle)
        {
            using (var context = new LabRestauranteEntities())
            {
                context.DetalleVenta.Add(detalle);
                context.SaveChanges();
                return detalle.id;
            }
        }

        public static List<DetalleVenta> listarPorVenta(int idVenta)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.DetalleVenta.Where(x => x.idVenta == idVenta && x.estado != -1).ToList();
            }
        }

        public static List<paDetalleVentaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.paDetalleVentaListar(parametro).ToList();
            }
        }
    }
}
