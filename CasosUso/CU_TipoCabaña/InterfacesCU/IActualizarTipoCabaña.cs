﻿using Dominio_Interfaces.EnitdadesNegocio;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_TipoCabaña.InterfacesCU
{
    public interface IActualizarTipoCabaña
    {
        void ActualizarTipoCabaña(TipoCabañaDTO aActualizar);
    }
}
