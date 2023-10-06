using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ValueObjects.Usuario
{
    public class ApellidoUsuarioVO
    {
        [Required]
        public string Valor { get; private set; }

        public ApellidoUsuarioVO(string value)
        {
            Valor = value;
            validar();
        }
        public ApellidoUsuarioVO()
        {
            
        }
        public void validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new ExcepcionesUsuario("El apellido no puede ser nulo");
            }
            if (Valor.Length<5)
            {
                throw new ExcepcionesUsuario("El apellido debe tener minimo 5 caracteres");
            }
        }

    }
}
