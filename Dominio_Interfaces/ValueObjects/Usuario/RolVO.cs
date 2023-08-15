using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ValueObjects.Usuario
{
    public class RolVO
    {
        public string Valor { get; set; }

        public RolVO(string value)
        {
            Valor = value;
            Validar();
        }
        public RolVO()
        {
            
        }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new ExcepcionesUsuario("Rol es obligatorio");
            }
            if(Valor.ToLower()!="usuario" || Valor.ToLower() != "administrador")
            {
                throw new ExcepcionesUsuario("Rol es puede ser usuario o administrador");
            }
        }
    }
}
