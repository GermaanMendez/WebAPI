﻿using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario.CUInterfaces
{
    public interface IListarAlquileresRealizadosPorUsuario
    {
        IEnumerable<AlquilerCabañaDTO> ListarAlquileresRealizadosPorUsuario(string emailUsuario);
    }
}
