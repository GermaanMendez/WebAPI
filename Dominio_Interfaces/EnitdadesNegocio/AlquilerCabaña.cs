using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias;

namespace Dominio_Interfaces.EnitdadesNegocio
{
    public class AlquilerCabaña
    {
        [Key]
        public int IdAlquiler { get; set; }

        public DateTime FechaAlquilerDesde { get; set; }
        public DateTime FechaAlquilerHasta { get; set; }
        public int Precio { get; set; }

        [Required]
        public Cabaña Cabaña { get; set; }
        [ForeignKey("Cabaña")]
        public int CabañaId { get; set; }

        [Required]
        public Usuario Usuario { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public void AsignarPrecio()
        {
            TimeSpan cantidadDiasAlquiler = FechaAlquilerHasta - FechaAlquilerDesde;
            int diferenciaEnDias = cantidadDiasAlquiler.Days;

            this.Precio = Cabaña.PrecioPorDia * diferenciaEnDias;
        }
        public void Validar()
        {
            AsignarPrecio();
            if (Precio < 1)
            {
                TimeSpan cantidadDiasAlquiler = FechaAlquilerHasta - FechaAlquilerDesde;
                int diferenciaEnDias = cantidadDiasAlquiler.Days;
                throw new ExepcionesAlquileresCabaña("The price cannot be less than 1" + Cabaña.PrecioPorDia+"  +  " + diferenciaEnDias);
            }
            if(FechaAlquilerDesde==null || FechaAlquilerHasta==null || FechaAlquilerDesde > FechaAlquilerHasta)
            {
                throw new ExepcionesAlquileresCabaña("The Dates cannot be null and the Start Date cannot be less than the From Date");
            }
            if(UsuarioId == Cabaña.Usuario.Id)
            {
                throw new ExepcionesAlquileresCabaña("The cabin owner cannot Rentaltheir own cabins");
            }
        }
    }

}
