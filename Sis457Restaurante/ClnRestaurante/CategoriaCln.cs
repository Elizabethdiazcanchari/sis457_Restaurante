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

                // Asegurar valores requeridos antes de Insert
                if (string.IsNullOrEmpty(categoria.usuarioRegistro))
                    categoria.usuarioRegistro = Environment.UserName; // o el usuario actual de la app
                if (categoria.fechaRegistro == default(DateTime))
                    categoria.fechaRegistro = DateTime.Now;

                context.Categoria.Add(categoria);
                context.SaveChanges();
                return categoria.id;
            }
        }

        public static int actualizar(Categoria categoria)
        {
            using (var context = new LabRestauranteEntities())
            {
                // Buscamos el registro original directamente en la base de datos por su ID
                var existente = context.Categoria.Find(categoria.id);

                if (existente != null)
                {
                    // Actualizamos únicamente los campos necesarios
                    existente.nombre = categoria.nombre;

                    // Campos de auditoría para el control de modificaciones
                    existente.usuarioRegistro = Environment.UserName;
                    existente.fechaRegistro = DateTime.Now;

                    // Guardamos los cambios y retornamos el número de filas afectadas
                    return context.SaveChanges();
                }

                return 0; // Si no encontró la categoría a modificar
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
