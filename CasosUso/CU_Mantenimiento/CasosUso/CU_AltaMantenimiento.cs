using CasosUso.CU_Mantenimiento.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Mantenimiento.CasosUso
{
    public class CU_AltaMantenimiento:IAltaMantenimiento
    {
       IRepositorioMantenimiento RepoMantenimiento { get; set; }

        public CU_AltaMantenimiento(IRepositorioMantenimiento repo)
        {
            RepoMantenimiento = repo;
        }

        public void AltaMantenimiento(MantenimientoDTO nuevoMantDTO, string email)
        {
            Mantenimiento nuevoMant = nuevoMantDTO.ToMantenimiento();
            nuevoMant.Validar();
            RepoMantenimiento.AddMaintenance(nuevoMant,email);
            nuevoMantDTO.Id = nuevoMant.Id;
        }
    }
}
