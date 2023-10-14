using Datos.ContextoEF;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using ExcepcionesPropias;
using Microsoft.EntityFrameworkCore;
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
            var MailUsuarioIniciaSesion = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor.ToLower() == mail.ToLower());
            if (MailUsuarioIniciaSesion!=null)
            {
                var UsuarioIniciaSesion = Contexto.Usuarios.Where(usu =>usu.Contraseña.Valor == contraseña && usu.Email.Valor.ToLower() == mail.ToLower()).FirstOrDefault();
                if (UsuarioIniciaSesion!=null)
                {
                   return UsuarioIniciaSesion;
                }
                else
                {
                    throw new ExcepcionesUsuario("Incorrect Password.");
                }
            }
            else
            {
                throw new ExcepcionesUsuario("There is not any user with that email, try again.");
            }
        }

        public Usuario RegistrarUsuario(Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                throw new ExcepcionesUsuario("The user to add cannot be null");
            }
            else
            {
                try
                {
                    var existeUsuario = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor == nuevoUsuario.Email.Valor);
                    if (existeUsuario == null)
                    {
                        Contexto.Usuarios.Add(nuevoUsuario);
                        Contexto.SaveChanges();
                        string mailNuevo = nuevoUsuario.Email.Valor.ToLower();
                        var usuarioAgregado = Contexto.Usuarios.FirstOrDefault(usuario => usuario.Email.Valor == mailNuevo);
                        return usuarioAgregado;
                    }
                    else
                    {
                        throw new ExcepcionesUsuario("A user with that email already exists in the system");
                    }
                }
                catch (ExcepcionesBaseDeDatos ex)
                {
                    throw new ExcepcionesBaseDeDatos("Error:" + ex.Message);
                }
            }
        }
            public Usuario GetUsuarioByEmail(string email)
            {
                var resultado = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == email.ToLower()).FirstOrDefault();
                return resultado;
            }
            public Usuario GetUsuarioById(int id)
            {
                var resultado = Contexto.Usuarios.Where(usu => usu.Id==id).FirstOrDefault();
                return resultado;
            }
            public IEnumerable<Usuario> GetAllUsuarios()
            {
                var resultado = Contexto.Usuarios.ToList();
                return resultado;
            }

            public IEnumerable<Cabaña> UserListedCabins(string email)
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ExcepcionesUsuario("The provided email is null");
                }
                else
                {
                   Usuario owner = Contexto.Usuarios.Where(usu => usu.Email.Valor.ToLower() == email.ToLower()).FirstOrDefault();
                if (owner == null) { throw new ExcepcionesUsuario("The user doesn't exists in the system"); }

                    var result = Contexto.Cabañas.Where(cab => cab.Usuario.Id== owner.Id).ToList();
                    return result;
                }
            }


            public IEnumerable<AlquilerCabaña> ObtenerAlquileresRealizadosPorUsuario(string emailUsuario)
            {
                try
                {
                    if (emailUsuario == null || emailUsuario.Length < 1) throw new ExepcionesAlquileresCabaña("invalid credentials");
                    var user = GetUsuarioByEmail(emailUsuario);
                    if (user == null) throw new ExepcionesAlquileresCabaña("The User does not exist");
                    var resultado = Contexto.AlquileresCabañas.Include(alq => alq.Cabaña).Include(alq=>alq.Usuario).Where(alq => alq.Usuario == user).ToList();
                    return resultado;
                }
                catch (ExcepcionesBaseDeDatos ex)
                {
                    throw new ExcepcionesBaseDeDatos("Error connecting to database " + ex.Message);
                }
            }







    }
}
