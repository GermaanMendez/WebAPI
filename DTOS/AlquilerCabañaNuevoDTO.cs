using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class AlquilerCabañaNuevoDTO
    {
        public DateTime FechaAlquilerDesde { get; set; }
        public DateTime FechaAlquilerHasta { get; set; }
        public int Precio { get; set; }

        public int CabañaId { get; set; }
        public int UsuarioId { get; set; }

        public AlquilerCabaña ToAlquilerCabaña()
        {
            return new AlquilerCabaña()
            {
                FechaAlquilerDesde = this.FechaAlquilerDesde,
                FechaAlquilerHasta = this.FechaAlquilerHasta,
                Precio = this.Precio,
                CabañaId = this.CabañaId,
                UsuarioId = this.UsuarioId,
            };
        }



    }
}
