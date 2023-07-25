﻿using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Usuario;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
      
        I_iniciarSesionUsuario CU_IniciarSesionUsuario { get; set; }
        public UsuarioController(I_iniciarSesionUsuario cuLogin)
        {
            CU_IniciarSesionUsuario = cuLogin;
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Login de usuario
        /// </summary>
        /// <param name="usu"> Objeto usuario a loguearse en formato json </param>
        /// <returns>Retornará 200 OK: si el usuariio se logueo correctamente
        /// 401 Unauthorized  si los datos de logueo (ej: contrseña) son incorrectos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        #endregion
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioDTO usu)
        {
            try
            {
                UsuarioDTO logueado = CU_IniciarSesionUsuario.IniciarSesionUsuario(usu.Email, usu.Contraseña);

                    string token = ManejadorJWT.GenerarToken(logueado);
                    return Ok(new {Token = token });
            }
            catch (ExcepcionesUsuario ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}