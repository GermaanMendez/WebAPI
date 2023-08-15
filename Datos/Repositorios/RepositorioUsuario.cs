using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioUsuario:IRepositorioUsuario
    {
        LibreriaContexto Contexto { get; set; }
        public RepositorioUsuario(LibreriaContexto ctx)
        {
            Contexto=ctx;
        }

        public Usuario IniciarSesion(string mail, string contraseña)
        {
            var MailUsuarioIniciaSesion = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor == mail.ToLower());
            if (MailUsuarioIniciaSesion!=null)
            {
                var UsuarioIniciaSesion = Contexto.Usuarios.Where(usu =>usu.Contraseña.Valor == contraseña && usu.Email.Valor.ToLower() == mail.ToLower()).FirstOrDefault();
                if (UsuarioIniciaSesion!=null)
                {
                   return UsuarioIniciaSesion;
                }
                else
                {
                    throw new ExcepcionesUsuario("Contraseña incorrecta.");
                }
            }
            else
            {
                throw new ExcepcionesUsuario("No existe un usuario con el Email ingresado, intentelo de nuevo.");
            }
        }

        public Usuario RegistrarUsuario(Usuario nuevoUsuario)
        {
            if(nuevoUsuario==null)
            {
                throw new ExcepcionesUsuario("Los datos para el registro no pueden ser nulos");
            }
            else
            {
                try
                {
                    var existeUsuario = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor == nuevoUsuario.Email.Valor);
                    if (existeUsuario == null) {
                            Contexto.Usuarios.Add(nuevoUsuario);
                            Contexto.SaveChanges();
                            string mailNuevo = nuevoUsuario.Email.Valor.ToLower();
                            var usuarioAgregado = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor == mailNuevo);
                            return usuarioAgregado;
                    }
                    else
                    {
                        throw new ExcepcionesUsuario("Ya existe un usuario con ese email");
                    }
                }
                catch (ExcepcionesBaseDeDatos ex)
                {
                    throw new ExcepcionesBaseDeDatos("Error:" + ex.Message);
                }
            }
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            var resultado = Contexto.Usuarios.ToList();
            return resultado;
        }
    }
}
