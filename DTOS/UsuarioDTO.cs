using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Contraseña { get; set; }


    }
}
