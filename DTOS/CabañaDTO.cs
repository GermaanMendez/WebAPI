using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ValueObjects.Cabaña;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTOS
{
    public class CabañaDTO
    {
        public int NumeroHabitacion { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public string Descripcion { get; set; }
        public bool PoseeJacuzzi { get; set; }
        public bool EstaHabilitada { get; set; }
        public int CantidadPersonasMax { get; set; }
        public int PrecioDiario { get; set; }
        public int IdTipoCabaña { get; set; }
        public TipoCabaña? tipoCabaña { get; set; }

        public int IdUsuario { get; set; }
        public UsuarioDTO? Usuario { get; set; }




        public Cabaña ToCabaña()
        {
            return new Cabaña()
            {
                NumeroHabitacion=this.NumeroHabitacion,
                Nombre = new NombreCabaña(this.Nombre),
                Foto = this.Foto,
                Descripcion = new DescripcionCabaña(this.Descripcion),
                PoseeJacuzzi = this.PoseeJacuzzi,
                EstaHabilitada = this.EstaHabilitada,
                CantidadPersonasMax = this.CantidadPersonasMax,
                TipoCabaña = this.tipoCabaña,
                IdTipoCabaña = this.IdTipoCabaña,
                Usuario = this.Usuario.ToUsuario(),
                //IdUsuario=this.IdUsuario,
                PrecioPorDia=this.PrecioDiario,
            };
        }
    }
}
