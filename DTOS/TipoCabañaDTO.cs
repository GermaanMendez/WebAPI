﻿using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesDominio;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ValueObjects.Cabaña;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTOS
{
    public class TipoCabañaDTO
    {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }


            public TipoCabaña ToTipoCabaña()
            {
                var TipoCabaña = new TipoCabaña()
                {
                    Id = this.Id,
                    Descripcion = this.Descripcion,
                };
                    TipoCabaña.SetNombre(this.Nombre);
                return TipoCabaña;
            }


    }
}
