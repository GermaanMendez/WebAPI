using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.InterfacesCU
{
    public interface IListarAlquileresDeMiCabañaDueño
    {
        IEnumerable<AlquilerCabañaDTO> ListarAlquileresDeMiCabañaDueño(string emailUsuario,int idCabaña);
    }
}
