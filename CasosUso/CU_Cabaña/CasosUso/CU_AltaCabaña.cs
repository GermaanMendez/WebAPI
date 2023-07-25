using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_AltaCabaña : IAltaCabaña
    {
        IRepositorioCabaña RepoCabaña { get; set; }
        IRepositorioParametros RepoParam { get; set; }
        public CU_AltaCabaña(IRepositorioCabaña repoCab, IRepositorioParametros repoParam)
        {
            RepoCabaña = repoCab;  
            RepoParam = repoParam;
        }
        public void AltaCabaña(CabañaDTO nuevo)
        {
            try
            {
                Cabaña nuevaCabaña = nuevo.ToCabaña();
                nuevaCabaña.Validar();
                RepoCabaña.Add(nuevaCabaña);
                nuevo.NumeroHabitacion = nuevaCabaña.NumeroHabitacion;
            }
            catch (Exception ex)
            {
                throw new ExcepcionesCabaña("ErrorCU_ALTA: " + ex.Message);
            }
        }
    }
}
