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
        [RegularExpression(@"^[a-zA-ZñÑ]+([a-zA-ZñÑ ]*[a-zA-ZñÑ])?$", ErrorMessage = "El campo Nombre debe contener solamente caracteres alfabéticos y espacios embebidos, pero no al principio o al final.")]
        [Column(TypeName ="nvarchar(100)")]
        private string Nombre { get; set; }
        public string Descripcion { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new ExcepcionesTipoCabaña("El nombre no puede estar vacío");
            }
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new ExcepcionesTipoCabaña("La descripcion no puede estar vacía");
            }
            if (Descripcion.Length < CantMinCarDescripcionTipoCabaña)
            {
                throw new ExcepcionesTipoCabaña("La descripción debe tener minimo: " + CantMinCarDescripcionTipoCabaña + " caracteres.");
            }
            if (Descripcion.Length > CantMaxCarDescripcionTipoCabaña)
            {
                throw new ExcepcionesTipoCabaña("La descripción debe maximo minimo: " + CantMaxCarDescripcionTipoCabaña + " caracteres.");
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
