using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio_Interfaces.ValueObjects.Cabaña;

namespace DTOS
{
    public class MantenimientoDTO
    {
        public int Id { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public double CostoMantenimiento { get; set; }
        public string NombreEmpleado { get; set; }
        public int IdCabaña { get; set; }

        public Mantenimiento ToMantenimiento()
        {
            return new Mantenimiento()
            {
                Id = this.Id,
                FechaMantenimiento = this.FechaMantenimiento,
                Descripcion = this.Descripcion,
                CostoMantenimiento = this.CostoMantenimiento,
                NombreEmpleado = this.NombreEmpleado,
                IdCabaña = this.IdCabaña,
            };
        }
    }
}
