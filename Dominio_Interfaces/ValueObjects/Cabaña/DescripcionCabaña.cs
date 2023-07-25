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
                throw new ExcepcionesCabaña("La descripcion no puede estar vacia");
            }
            if (valor.Length < CantMinCarDescripcionCabaña)
            {
                throw new ExcepcionesCabaña("La descripción debe tener minimo: " + CantMinCarDescripcionCabaña + " caracteres.");
            }
            if (valor.Length > CantMaxCarDescripcionCabaña)
            {
                throw new ExcepcionesCabaña("La descripción debe maximo minimo: " + CantMaxCarDescripcionCabaña + " caracteres.");
            }
        }
    }   
}
