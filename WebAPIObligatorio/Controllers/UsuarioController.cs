using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using ExcepcionesPropias;
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
        IGetUsuarioByEmail CU_GetUsuarioByEmail { get; set; }
        IListarCabañasListadasPorDueño CU_GetCabinsListedForRentByOwner { get; set; }
        IListarAlquileresRealizadosPorUsuario CU_ListarAlquileresRealizadosUsuario { get; set; }

        public UsuarioController(I_iniciarSesionUsuario cuLogin, I_RegistrarUsuario cuRegistro, I_ListarUsuarios cU_ListarUsuarios, IGetUsuarioByEmail cU_GetUsuarioByEmail,IListarCabañasListadasPorDueño gerCabinsListedForRentByOwner,IListarAlquileresRealizadosPorUsuario cuListarAlquileresUsuario)
        {
            CU_IniciarSesionUsuario = cuLogin;
            CU_RegistrarUsuario = cuRegistro;
            CU_ListarUsuarios = cU_ListarUsuarios;
            CU_GetUsuarioByEmail = cU_GetUsuarioByEmail;
            CU_GetCabinsListedForRentByOwner = gerCabinsListedForRentByOwner;
            CU_ListarAlquileresRealizadosUsuario = cuListarAlquileresUsuario;
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
        public IActionResult Login([FromBody] UsuarioLoginDTO usu)
        {
            try
            {
                UsuarioDTO logueado = CU_IniciarSesionUsuario.IniciarSesionUsuario(usu.Email, usu.Contraseña);

                    string rolUsuario = logueado.Rol;
                    string token = ManejadorJWT.GenerarToken(logueado);
                    return Ok(new {Token = token,Rol=rolUsuario });
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
            if (usu == null) return BadRequest("The user to create cannot be null");
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
                if (!usuarios.Any()) return NotFound("There is not any user in the System");
                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(500, " An unexpected error occurred");
            }
        }
        [HttpGet("Usuario/${email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                UsuarioDTO usuario = CU_GetUsuarioByEmail.GetUsuarioByEmail(email);
                if (usuario == null) { return BadRequest("There is not any user with that email"); }
                else { return Ok(usuario); }
            }
            catch
            {
                return StatusCode(500, " An unexpected error occurred");
            }
        }
        [HttpGet("Listadas/${email}")]
        public IActionResult GetCabinsListedForRentByOwner(string email)
        {
            try
            {
                UsuarioDTO usuario = CU_GetUsuarioByEmail.GetUsuarioByEmail(email);
                if (usuario == null) { return BadRequest("The user with this email doesn't exists in the system"); }
                else
                {
                    IEnumerable<CabañaDTO> cabinsOfOwner = CU_GetCabinsListedForRentByOwner.UserListedCabins(email);
                    if (!cabinsOfOwner.Any())
                    {
                        return NotFound("The user doesn't have cabins listed to rent");
                    }
                    else
                    {
                        return Ok(cabinsOfOwner);
                    }
                }
            }
            catch
            {
                return StatusCode(500, "Unexpected Error");
            }
        }



        [HttpGet("alquileresUsuario/{emailUsuario}")]
        public IActionResult GetListarAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario)) return BadRequest("The email of the user cannot be null");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresRealizadosUsuario.ListarAlquileresRealizadosPorUsuario(emailUsuario);
                if (!alquileres.Any()) return NotFound("There is not any rents realized for the selected user");
                return Ok(alquileres);
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "Unexpected Error");
            }
        }











    }
}
