using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ExepcionesPropias
{
    public class ExcepcionesTipoCabaña : Exception
    {
        public ExcepcionesTipoCabaña() : base()
        {
        }

        public ExcepcionesTipoCabaña(string? message) : base(message)
        {
        }

        public ExcepcionesTipoCabaña(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
