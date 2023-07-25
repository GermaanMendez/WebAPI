using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesDominio;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.EnitdadesNegocio
{
    public class Mantenimiento : IValidable
    {
        public int Id { get; set; }
        [Display(Name = "Fecha del mantenimiento")]
        public DateTime FechaMantenimiento  { get; set; }

        //[Range(10, 200, ErrorMessage = "Error, la descripción debe tener entre 10 y 200 caracteres")]
        [StringLength(200, MinimumLength = 10)]
        public string Descripcion { get; set; }
        [Display(Name = "Costo del mantenimiento")]
        public double CostoMantenimiento { get; set; }
        [Display(Name = "Nombre del empleado")]
        [Column(TypeName = "nvarchar(50)")]
        public string NombreEmpleado { get; set; } 
        public Cabaña Cabaña { get; set; }
        [ForeignKey("Cabaña")] 
        public int IdCabaña { get; set; }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new ExcepcionesMantenimiento("La descripcion no puede estar vacia");
            }
            if (Descripcion.Length < 10)
            {
                throw new ExcepcionesMantenimiento("La descripción no puede tener menos de 10 carácteres.");
            }
            if (Descripcion.Length > 200)
            {
                throw new ExcepcionesMantenimiento("La descripción no puede tener mas de 200 carácteres.");
            }
            if (CostoMantenimiento<1)
            {
                throw new ExcepcionesMantenimiento("El costo de mantenimiento no puede ser menor a 1");
            }
            if (string.IsNullOrEmpty(NombreEmpleado))
            {
                throw new ExcepcionesMantenimiento("El nombre del emppleado de mantenimiento no puede estar vacio");
            }
        }
    }
}
