using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio_Interfaces.ValueObjects.Cabaña
{
    public class NombreCabaña
    {
        public string valor { get; private set; }

        public NombreCabaña(string val)
        {
            valor = val;
            Validar();
        }
        private NombreCabaña()
        {
        }
        public void Validar()
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new ExcepcionesCabaña("El nombre no puede estar vacío");
            }
        }
    }
}
