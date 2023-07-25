using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ExepcionesPropias
{
    public class ExcepcionesCabaña : Exception
    {
        public ExcepcionesCabaña():base()
        {
        }

        public ExcepcionesCabaña(string? message) : base(message)
        {
        }

        public ExcepcionesCabaña(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
