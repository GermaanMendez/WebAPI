using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorioAlquilerCabaña:IRepositorio<AlquilerCabaña>
    {
        void EditarAlquilerCabaña(string emailUsuario, AlquilerCabaña obj);
        bool EliminarAlquilerCabaña(string emailUsuario, int idAlquiler);
        IEnumerable<AlquilerCabaña> ObtenerAlquileresRealizadosPorUsuario(string emailUsuario);
        IEnumerable<AlquilerCabaña> ObtenerAlquileresDeMiCabaña(string emailUsuario,int idCabaña);

    }
}
