using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ExepcionesPropias
{
    public class ExcepcionesUsuario : Exception
    {
        public ExcepcionesUsuario():base()
        {
        }

        public ExcepcionesUsuario(string? message) : base(message)
        {
        }

        public ExcepcionesUsuario(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
