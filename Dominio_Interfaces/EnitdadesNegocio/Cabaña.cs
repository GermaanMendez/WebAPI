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
        [RegularExpression(@"^[a-zA-ZñÑ]+([a-zA-ZñÑ ]*[a-zA-ZñÑ])?$", ErrorMessage = "The Name field must contain only alphabetic characters and embedded spaces, but not at the beginning or end.")]
        public NombreCabaña Nombre { get; set; }
        public string Foto { get; set; }
        public DescripcionCabaña Descripcion { get; set; }
        [Display(Name = "Posee Jacuzzi")]
        public bool PoseeJacuzzi { get; set; }
        [Display(Name = "Está habilitada")]
        public bool EstaHabilitada { get; set; }
        [Display(Name = "Cantidad maxima de personas")]
        public int CantidadPersonasMax { get; set; }
        [Display(Name = "Precio Por Dia")]
        public int PrecioPorDia { get; set; }
        public TipoCabaña? TipoCabaña { get; set; }
        [ForeignKey("TipoCabaña")]
        public int IdTipoCabaña { get; set; }
        
        public Usuario? Usuario { get; set; }
        public void Validar()
        {
            if (CantidadPersonasMax<1)
            {
                throw new ExcepcionesCabaña("The cabin must have a minimum capacity of 1 person.");
            }
        }

  

    }
}
