using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.InterfacesCU
{
    public interface IEditarAlquilerCabaña
    {
        void EditarAlquilerCabaña(AlquilerCabañaDTO alquiler, string emailUsuarioAlquilo);
    }
}
