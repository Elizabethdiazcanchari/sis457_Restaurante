using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class CategoriaCln
    {
        public static List<Categoria> listar()
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Categoria
                    .Where(x => x.estado == 1)
                    .OrderBy(x => x.nombre)
                    .ToList();
            }
        }

        public static int insertar(Categoria categoria)
        {
            using (var context = new LabRestauranteEntities())
            {
                var existente = context.Categoria.FirstOrDefault(c => c.nombre.ToLower() == categoria.nombre.ToLower());
                if (existente != null)
                {
                    if (existente.estado == -1)
                    {
                        existente.estado = 1;
                        context.SaveChanges();
                        return existente.id;
                    }
                }
                context.Categoria.Add(categoria);
                context.SaveChanges();
                return categoria.id;
            }
        }

        public static int eliminar(int id)
        {
            using (var context = new LabRestauranteEntities())
            {
                var categoria = context.Categoria.Find(id);
                categoria.estado = -1;
                return context.SaveChanges();
            }
        }

        public static Categoria obtenerUno(int id)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.Categoria.Find(id);
            }
        }
    }
}
