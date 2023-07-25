using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesDominio;
using Dominio_Interfaces.ValueObjects.Cabaña;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.EnitdadesNegocio
{
    //[Index(nameof(Nombre), IsUnique = true)]
    public class Cabaña:IValidable
    {
        [Key]
        public int NumeroHabitacion { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZñÑ]+([a-zA-ZñÑ ]*[a-zA-ZñÑ])?$", ErrorMessage = "El campo Nombre debe contener solamente caracteres alfabéticos y espacios embebidos, pero no al principio o al final.")]
        public NombreCabaña Nombre { get; set; }
        public string Foto { get; set; }
        public DescripcionCabaña Descripcion { get; set; }
        [Display(Name = "Posee Jacuzzi")]
        public bool PoseeJacuzzi { get; set; }
        [Display(Name = "Está habilitada")]
        public bool EstaHabilitada { get; set; }
        [Display(Name = "Cantidad maxima de personas")]
        public int CantidadPersonasMax { get; set; }
        

        public TipoCabaña TipoCabaña { get; set; }
        [ForeignKey("TipoCabaña")]
        public int IdTipoCabaña { get; set; }

        public void Validar()
        {
            if (CantidadPersonasMax<1)
            {
                throw new ExcepcionesCabaña("La cabaña debe tener una capacidad minima de 1 persona.");
            }
        }
    }
}
