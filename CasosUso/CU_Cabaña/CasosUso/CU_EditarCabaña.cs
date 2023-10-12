using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_EditarCabaña : IEditarCabaña
    {
        IRepositorioCabaña repoCabaña { get;set; }
        public CU_EditarCabaña(IRepositorioCabaña rpo)
        {
            repoCabaña = rpo;
        }

        public void edit(CabañaDTO aEditar)
        {
            Cabaña ediatda = aEditar.ToCabaña();
            ediatda.Validar();
            repoCabaña.Update(ediatda);
        }
    }
}
