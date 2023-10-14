using Dominio_Interfaces.ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.ValueObjects.Usuario
{
    public class EmailUsuarioVO
    {
        [EmailAddress]
        [Required]
        public string Valor { get; private set; }

        public EmailUsuarioVO(string val)
        {
            Valor = val;
            Validar();
        }
        private EmailUsuarioVO()
        {
        }
        public void Validar()
        {
         if (string.IsNullOrEmpty(Valor))
          {
            throw new ExcepcionesUsuario("The Email cannot be null");
          }
            if (Valor.Length < 5)
            {
                throw new ExcepcionesUsuario("The Email must be have minimum 5 characters");
            }
        }

    }

}
