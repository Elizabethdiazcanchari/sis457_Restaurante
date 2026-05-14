using CadRestaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRestaurante
{
    public class EmpleadoCln
    {
        public static int crear(Empleado empleado, Usuario usuario)
        {
            using (var context = new LabRestauranteEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();
                if (usuario != null && UsuarioCln.obtenerUnoPorEmpleado(empleado.id) == null)
                {
                    usuario.idEmpleado = empleado.id;
                    usuario.usuarioRegistro = empleado.usuarioRegistro;
                    usuario.fechaRegistro = empleado.fechaRegistro;
                    usuario.estado = empleado.estado;
                    UsuarioCln.crear(usuario);
                }
                return empleado.id;
            }
        }
        public static int actualizar(Empleado empleado, string nombreUsuario, string clave)
        {
            using (var context = new LabRestauranteEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.cedulaIdentidad = empleado.cedulaIdentidad;
                existente.nombres = empleado.nombres;
                existente.primerApellido = empleado.primerApellido;
                existente.segundoApellido = empleado.segundoApellido;
                existente.fechaNacimiento = empleado.fechaNacimiento;
                existente.direccion = empleado.direccion;
                existente.celular = empleado.celular;
                existente.cargo = empleado.cargo;
                existente.usuarioRegistro = empleado.usuarioRegistro;

                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    var usuario = UsuarioCln.obtenerUnoPorEmpleado(existente.id);
                    if (usuario != null)
                    {
                        usuario.usuario1 = nombreUsuario;
                        usuario.usuarioRegistro = empleado.usuarioRegistro;
                        UsuarioCln.actualizar(usuario);
                    }
                    else
                    {
                        usuario = new Usuario
                        {
                            idEmpleado = existente.id,
                            usuario1 = nombreUsuario,
                            clave = clave,
                            estado = 1,
                            fechaRegistro = DateTime.Now,
                            usuarioRegistro = empleado.usuarioRegistro
                        };
                        UsuarioCln.crear(usuario);
                    }
                }

                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabRestauranteEntities())
            {
                var empleado = context.Empleado.Find(id);
                empleado.estado = -1;
                empleado.usuarioRegistro = usuario;

                var usuarioEmpleado = UsuarioCln.obtenerUnoPorEmpleado(empleado.id);
                if (usuarioEmpleado != null)
                {
                    UsuarioCln.eliminar(usuarioEmpleado.id, usuario);
                }

                return context.SaveChanges();
            }
        }
        public static Empleado obtenerUno(int id)
        {
            using (var context = new LabRestauranteEntities())
            {
                // El método Include espera un string con el nombre de la propiedad de navegación
                return context.Empleado.Include("Usuario").FirstOrDefault(x => x.id == id);
            }
        }
        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabRestauranteEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }
    }
}
