using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class ExepcionesAlquileresCabaña:Exception
    {
        public ExepcionesAlquileresCabaña() : base()
        {
        }

        public ExepcionesAlquileresCabaña(string? message) : base(message)
        {
        }

        public ExepcionesAlquileresCabaña(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
