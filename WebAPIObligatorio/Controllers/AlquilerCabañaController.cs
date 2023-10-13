using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_TipoCabaña.CasosUso;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerCabañaController : Controller
    {
        public IBuscarAlquilerPorId CU_BuscarAlquilerPorId { get; set; }
        public IAltaAlquilerCabaña CU_AltaAlquilerCabaña { get; set; }
        public IEliminarAlquilerCabaña CU_EliminarAlquilerCabaña { get; set; }
        public IEditarAlquilerCabaña CU_EditarAlquilerCabaña { get; set; }
        public IListarAlquileresDeMiCabañaDueño CU_ListarAlquileresDeMiCabañaDueño { get; set; }

        public AlquilerCabañaController(IAltaAlquilerCabaña cuAlta, IEliminarAlquilerCabaña cuBaja, IEditarAlquilerCabaña cuEditar, IListarAlquileresDeMiCabañaDueño cuListarDueño, IListarAlquileresRealizadosPorUsuario cuListarAlquileresUsuario, IBuscarAlquilerPorId cuBuscarAlqId)
        {
            CU_AltaAlquilerCabaña = cuAlta;
            CU_EliminarAlquilerCabaña = cuBaja;
            CU_EditarAlquilerCabaña = cuEditar;
            CU_ListarAlquileresDeMiCabañaDueño = cuListarDueño;
            CU_BuscarAlquilerPorId = cuBuscarAlqId;
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Create Rental
        /// </summary>
        /// <param name="alquilerCabañaDto"> Rental object to Sign in in json format</param>
        /// <returns>Returns 200 OK with the user 
        /// 400 BadRequest If the Rentalis null - If the user who is creating the Rental is the owner of the cabin - If The cabin is not available to Rentalin that date range
        /// 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AlquilerCabañaNuevoDTO? alquilerCabañaDto)
        {
            if (alquilerCabañaDto == null) return BadRequest("The Rental to be created cannot be null");
            try
            {
                CU_AltaAlquilerCabaña.AltaAlquilerCabaña(alquilerCabañaDto);
                return Ok("Rental created successfully");
            }
            catch (ExepcionesAlquileresCabaña ex)
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
        /// Delete Rental
        /// </summary>
        /// <param name="emailDueño"> Email of the user that is trying to delete the Rental</param>
        /// <param name="idAlquilerABorraar">Id of the Rentalto delete</param>
        /// <returns>Returns 200 OK for Success
        /// 400 BadRequest If the email or Id are null - If the user who is deleting the Rental  does not exists in the system - If the Rental does not exists in the system
        /// 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpDelete("{emailDueño}/{idAlquilerABorraar}")]
        [Authorize]
        public IActionResult Delete(string emailDueño, int idAlquilerABorraar)
        {
            if (string.IsNullOrEmpty(emailDueño) || idAlquilerABorraar <= 0) return BadRequest("A Rentalto be deleted and owner's email must be provided");
            try
            {
                CU_EliminarAlquilerCabaña.EliminarAlquilerCabaña(idAlquilerABorraar, emailDueño);
                return Ok();
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Could not remove Rental. Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }




        #region DOCUMENTACION API
        /// <summary>
        /// Get Rental
        /// </summary>
        /// <param name="id"> Id of the Rental to get </param>
        /// <returns>Returns 200 OK with the Rental
        /// 400 BadRequest If the Id is less than zero - 
        /// 404 Not Found If the Rental with that Id does not exists in the System
        /// 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet("{id}", Name = "RutaDetailAlquiler")]
        public IActionResult Get(int id) //DETAILS
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Id");
                AlquilerCabañaDTO buscado = CU_BuscarAlquilerPorId.ObtenerPorId(id);
                if (buscado == null) return NotFound($"The Rental with ID: {id} does not exist");
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Obtain rentals for a cabin that the user owns
        /// </summary>
        /// <returns>Returns 200 OK with the Rentals
        /// 400 BadRequest If the email is null or the Id is less than 1- If the cabin does not exists in the system - If the user does not exists in the system -If the user is not the owner of the cabin
        /// 404 Not Found If there is not any Rental for that cabin
        /// 500 Server Error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet("email/{emailUsuario}/cabaña/{idCabaña}")]
        public IActionResult GetListarAlquileresDeMiCabaña(string emailUsuario, int idCabaña)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario) || idCabaña < 1) return BadRequest("A valid email and Id of  cabin must be provided");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresDeMiCabañaDueño.ListarAlquileresDeMiCabañaDueño(emailUsuario, idCabaña);
                if (!alquileres.Any()) return NotFound("No Rental was found for the selected cabin");
                return Ok(alquileres);
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

 

    }
    }
