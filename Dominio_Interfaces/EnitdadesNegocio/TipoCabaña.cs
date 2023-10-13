using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.EnitdadesNegocio
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class TipoCabaña : IValidable
    {
        public static int CantMinCarDescripcionTipoCabaña { get; set; }
        public static int CantMaxCarDescripcionTipoCabaña { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZñÑ]+([a-zA-ZñÑ ]*[a-zA-ZñÑ])?$", ErrorMessage = "The Name field must contain only alphabetic characters and embedded spaces, but not at the beginning or end.")]
        [Column(TypeName ="nvarchar(100)")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new ExcepcionesTipoCabaña("The Name cannot be null");
            }
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new ExcepcionesTipoCabaña("The Description cannot be null");
            }
            if (Descripcion.Length < CantMinCarDescripcionTipoCabaña)
            {
                throw new ExcepcionesTipoCabaña("The description must have a minimum of: " + CantMinCarDescripcionTipoCabaña + " characters.");
            }
            if (Descripcion.Length > CantMaxCarDescripcionTipoCabaña)
            {
                throw new ExcepcionesTipoCabaña("The description must have a maxium of: " + CantMaxCarDescripcionTipoCabaña + " characters.");
            }
        }

        public string GetNombre()
        {
            return Nombre;
        }
        public void SetNombre(string nuevoNombre)
        {
            Nombre=nuevoNombre;
        }

    }
}
