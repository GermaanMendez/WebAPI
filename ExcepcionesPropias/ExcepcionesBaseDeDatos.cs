using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ExepcionesPropias
{
    public class ExcepcionesBaseDeDatos : Exception
    {
        public ExcepcionesBaseDeDatos():base()
        {
        }

        public ExcepcionesBaseDeDatos(string? message) : base(message)
        {
        }

        public ExcepcionesBaseDeDatos(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        


    }
}
