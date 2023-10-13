using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        IListarCabañasListadasPorDueño CU_GetCabinsListedForRentalByOwner { get; set; }
        IListarAlquileresRealizadosPorUsuario CU_ListarAlquileresRealizadosUsuario { get; set; }

        public UsuarioController(I_iniciarSesionUsuario cuLogin, I_RegistrarUsuario cuRegistro, I_ListarUsuarios cU_ListarUsuarios, IGetUsuarioByEmail cU_GetUsuarioByEmail,IListarCabañasListadasPorDueño gerCabinsListedForRentalByOwner,IListarAlquileresRealizadosPorUsuario cuListarAlquileresUsuario)
        {
            CU_IniciarSesionUsuario = cuLogin;
            CU_RegistrarUsuario = cuRegistro;
            CU_ListarUsuarios = cU_ListarUsuarios;
            CU_GetUsuarioByEmail = cU_GetUsuarioByEmail;
            CU_GetCabinsListedForRentalByOwner = gerCabinsListedForRentalByOwner;
            CU_ListarAlquileresRealizadosUsuario = cuListarAlquileresUsuario;
        }

        #region DOCUMENTACION API
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="usu">User object to log in json format </param>
        /// <returns>Returns 200 OK login successfully
        /// 401 Unauthorized If the passowrd or email are wrong</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// User Sign In
        /// </summary>
        /// <param name="usu"> User object to Sign in in json format</param>
        /// <returns>Returns 200 OK: Sign In successfully
        /// Returns 400 BadRequest If the user is null
        /// Returns 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Ger all Users
        /// </summary>
        /// <returns>Returns 200 OK: with the list of Users
        /// Returns 404 Not Found if is not exists any User in the system
        /// Returns 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
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

        #region DOCUMENTACION API
        /// <summary>
        /// Get User by email
        /// </summary>
        /// <param name="email"> Email of the User to search</param>
        /// <returns>Returns 200 OK with the user 
        /// Returns 400 BadRequest If the email is null
        /// Returns 404 Not Found if is not any user with that email
        /// Returns 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet("Usuario/${email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                if (email.IsNullOrEmpty()) return BadRequest("The email cannot be null");
                UsuarioDTO usuario = CU_GetUsuarioByEmail.GetUsuarioByEmail(email);
                if (usuario == null) { return NotFound("There is not any user with that email"); }
                else { return Ok(usuario); }
            }
            catch
            {
                return StatusCode(500, " An unexpected error occurred");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Get the cabins that the user has listed for Rental
        /// </summary>
        /// <param name="email"> Email of the User </param>
        /// <returns>Returns 200 OK with the list of Cabins 
        /// Returns 400 BadRequest If the email is null - If the user with that email does not exists in the system
        /// Returns 404 Not Found if is not any cabin listened to Rentalfor the user
        /// Returns 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet("Listadas/${email}")]
        public IActionResult GetCabinsListedForRentalByOwner(string email)
        {
            try
            {
                if(email.IsNullOrEmpty()) return BadRequest("The email cannot be null");
                UsuarioDTO usuario = CU_GetUsuarioByEmail.GetUsuarioByEmail(email);
                if (usuario == null) { return BadRequest("The user with this email doesn't exists in the system"); }
                else
                {
                    IEnumerable<CabañaDTO> cabinsOfOwner = CU_GetCabinsListedForRentalByOwner.UserListedCabins(email);
                    if (!cabinsOfOwner.Any())
                    {
                        return NotFound("The user doesn't have cabins listed to Rental");
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


        #region DOCUMENTACION API
        /// <summary>
        /// Get Rentals that the user has made
        /// </summary>
        /// <param name="emailUsuario"> Email of the User </param>
        /// <returns>Returns 200 OK with the list of Rentals
        /// Returns 400 BadRequest If the email is null
        /// Returns 404 Not Found if is not any Rental
        /// Returns 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet("alquileresUsuario/{emailUsuario}")]
        public IActionResult GetListarAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario)) return BadRequest("The email of the user cannot be null");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresRealizadosUsuario.ListarAlquileresRealizadosPorUsuario(emailUsuario);
                if (!alquileres.Any()) return NotFound("There is not any Rentals realized for the selected user");
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
