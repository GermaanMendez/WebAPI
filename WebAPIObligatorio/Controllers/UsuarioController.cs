using CasosUso.CU_Cabaña.CasosUso;
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
        I_RegistrarUsuario CU_RegistrarUsuario { get; set; }
        I_ListarUsuarios CU_ListarUsuarios { get; set; }    
        public UsuarioController(I_iniciarSesionUsuario cuLogin, I_RegistrarUsuario cuRegistro, I_ListarUsuarios cU_ListarUsuarios)
        {
            CU_IniciarSesionUsuario = cuLogin;
            CU_RegistrarUsuario = cuRegistro;
            CU_ListarUsuarios = cU_ListarUsuarios;
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

        #region DOCUMENTACION API
        /// <summary>
        /// Registro de usuario
        /// </summary>
        /// <param name="usu"> Objeto usuario a registrarse en formato json </param>
        /// <returns>Retornará 200 OK: si el usuariio se logueo correctamente
        /// 400 BadRequest  si los datos de logueo (ej: contrseña) son incorrectos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost ("Registro")]
        public IActionResult Registro([FromBody] UsuarioDTO usu)
        {
            if (usu == null) return BadRequest("No se puede registrar un usuario nulo");
            try
            {
               UsuarioDTO nuevo=CU_RegistrarUsuario.RegistrarUsuario(usu);
                string token = ManejadorJWT.GenerarToken(nuevo);
                return Ok(new {Token = token});
            }
            catch (ExcepcionesUsuario ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Usuarios")]
        public IActionResult Listar()
        {
            try
            {
                IEnumerable<UsuarioDTO> usuarios=CU_ListarUsuarios.GetUsuarios();
                if (!usuarios.Any()) return NotFound("No hay ningun usuario en el sistema");
                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(500, " Ocurrio un error inesperado");
            }
        }

    }
}
