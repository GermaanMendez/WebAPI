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
        //esto no estaba ademas cabañaId estaba arria de cabaña
        [ForeignKey("Cabaña")]
        public int CabañaId { get; set; }

        [Required]
        public Usuario Usuario { get; set; }
        //esto no estaba ademas usuarioId estaba arria de usuario
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
                throw new ExepcionesAlquileresCabaña("El precio no puede ser menor a 1" + Cabaña.PrecioPorDia+"  +  " + diferenciaEnDias);
            }
            if(FechaAlquilerDesde==null || FechaAlquilerHasta==null || FechaAlquilerDesde > FechaAlquilerHasta)
            {
                throw new ExepcionesAlquileresCabaña("Las Fechas no pueden ser vacias ademas la fecha de inicio debe ser menor a la fecha final");
            }
            if(UsuarioId == Cabaña.Usuario.Id)
            {
                throw new ExepcionesAlquileresCabaña("El dueño de la cabaña no puede alquilar sus propias cabañaas");
            }
        }
    }

}
