using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ValueObjects.Cabaña
{
    public class DescripcionCabaña
    {
        public static int CantMinCarDescripcionCabaña { get; set; }
        public static int CantMaxCarDescripcionCabaña { get; set; }
        public string valor { get; private set; }

        public DescripcionCabaña(string val)
        {
            valor = val;
            Validar();
        }
        private DescripcionCabaña()
        {
        }
        public void Validar()
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new ExcepcionesCabaña("The Description cannot be null");
            }
            if (valor.Length < CantMinCarDescripcionCabaña)
            {
                throw new ExcepcionesCabaña("The Description must be have minimum : " + CantMinCarDescripcionCabaña + " characters.");
            }
            if (valor.Length > CantMaxCarDescripcionCabaña)
            {
                throw new ExcepcionesCabaña("The Description must be have maxium : " + CantMaxCarDescripcionCabaña + " characters.");
            }
        }
    }   
}
