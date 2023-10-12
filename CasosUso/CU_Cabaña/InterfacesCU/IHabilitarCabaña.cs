using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.InterfacesCU
{
    public interface IHabilitarCabaña
    {
        bool HabiliarCabaña(string email, int idCabaña);
    }
}
