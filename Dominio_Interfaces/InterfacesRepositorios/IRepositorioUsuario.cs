﻿using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorioUsuario
    {
        Usuario IniciarSesion(string mail, string contraseña);
        Usuario RegistrarUsuario(Usuario nuevo);
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioByEmail(string email);
        Usuario GetUsuarioById(int id);
        IEnumerable<Cabaña> UserListedCabins(string email);
        IEnumerable<AlquilerCabaña> ObtenerAlquileresRealizadosPorUsuario(string emailUsuario);
    }
}
