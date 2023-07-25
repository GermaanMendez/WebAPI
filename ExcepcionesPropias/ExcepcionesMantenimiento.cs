using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ExepcionesPropias
{
    public  class ExcepcionesMantenimiento : Exception
    {
        public ExcepcionesMantenimiento() : base()
        {
        }

        public ExcepcionesMantenimiento(string? message) : base(message)
        {
        }

        public ExcepcionesMantenimiento(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
