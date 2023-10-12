using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_DeshabilitarCabaña : IDeshabilitarCabaña
    {
        IRepositorioCabaña RepoCabaña { get; set; }
        public CU_DeshabilitarCabaña(IRepositorioCabaña rpo)
        {
            RepoCabaña = rpo;
        }
        public bool DeshabilitarCabaña(string email, int idCabaña)
        {
            return RepoCabaña.DeshabilitarCabaña(email, idCabaña);
        }
    }
}
