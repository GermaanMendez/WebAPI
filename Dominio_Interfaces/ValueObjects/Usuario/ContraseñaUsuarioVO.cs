using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ValueObjects.Usuario
{
    public class ContraseñaUsuarioVO
    {
        public string Valor { get; private set; }
        public ContraseñaUsuarioVO(string val)
        {
            Valor= val;
            Validar();
        }
        private ContraseñaUsuarioVO()
        {
        }
        public void Validar() {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new ExcepcionesUsuario("La contraseña es obligatoria");
            }
        }
    }

}
