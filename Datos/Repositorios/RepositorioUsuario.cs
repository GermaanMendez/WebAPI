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
    }
}
