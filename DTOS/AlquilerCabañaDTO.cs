using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ValueObjects.Cabaña;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class AlquilerCabañaDTO
    {
        public DateTime FechaAlquilerDesde { get; set; }
        public DateTime FechaAlquilerHasta { get; set; }
        public int Precio { get; set; }

        public int CabañaId { get; set; }
        public Cabaña? Cabaña { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public AlquilerCabaña ToAlquilerCabaña()
        {
            return new AlquilerCabaña()
            {
                FechaAlquilerDesde = this.FechaAlquilerDesde,
                FechaAlquilerHasta = this.FechaAlquilerHasta,
                Precio = this.Precio,
                CabañaId = this.CabañaId,
                Cabaña = this.Cabaña,
                UsuarioId = this.UsuarioId,
                Usuario = this.Usuario
            };
        }

    }
}
