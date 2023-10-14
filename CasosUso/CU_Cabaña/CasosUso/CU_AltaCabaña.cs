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
        IRepositorioUsuario RepoUsu { get; set; }
        public CU_AltaCabaña(IRepositorioCabaña repoCab, IRepositorioUsuario rpoU)
        {
            RepoCabaña = repoCab;
            RepoUsu = rpoU;
        }
        public void AltaCabaña(CabañaDTO nuevo)
        {
            try
            {
                Cabaña nuevaCabaña = nuevo.ToCabaña();
                Usuario dueño = RepoUsu.GetUsuarioByEmail(nuevo.Usuario.Email);
                nuevaCabaña.Usuario = dueño;
                nuevaCabaña.Validar();
                RepoCabaña.Add(nuevaCabaña);
                nuevo.NumeroHabitacion = nuevaCabaña.NumeroHabitacion;
            }
            catch (Exception ex)
            {
                throw new ExcepcionesCabaña(ex.Message);
            }
        }
    }
}
