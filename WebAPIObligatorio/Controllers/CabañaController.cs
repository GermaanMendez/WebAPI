using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Parametros;
using CasosUso.CU_TipoCabaña.CasosUso;
using CasosUso.CU_TipoCabaña.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.ValueObjects.Cabaña;
using DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class CabañaController : ControllerBase
    {
        IAltaCabaña CU_AltaCabaña { get; set; }
        IEliminarCabaña CU_EliminarCabaña { get; set; } 
        IListarPorCantPersonas CU_ListarPorCantPersonas { get; set; }
        IListarPorNOHabilitadas CU_ListarPorHabilitadas { get; set; }
        IListarPorTexto CU_ListarPorTexto { get; set; }
        IListarPorTipo CU_ListarPorTipo { get; set; }
        IListarTodas CU_ListarTodas { get; set; }
        IListarPorMonto CU_ListarPorTipoYMonto { get; set; }
        IBuscarCabañaPorid CU_buscarCabañaPorId { get; set; }

        IListarTiposCabañas CU_ListarTiposCabañas { get; set; }
        IListarEnRangoFechas CU_ListarDisponiblesEnRango { get; set; }
        IObtenerValorParam CU_ObtenerValorParametro { get; set; }
        IGetUsuarioByEmail CU_ObtenerUsuarioPorEmail { get; set; }
        IEditarCabaña CU_EditarCabaña { get; set; }

        IBuscarTipoPorId CU_BuscarTipoPorId { get; set; }
        IHabilitarCabaña CU_HabilitarCabaña { get; set; }
        IDeshabilitarCabaña CU_DeshabilitarCabaña { get; set; }
        public IWebHostEnvironment WHE { get; set; }
        public CabañaController(IAltaCabaña cuAlta, IListarPorCantPersonas cuListPers, IListarPorNOHabilitadas cuListHab, IListarPorTexto cuLisTxt, IListarPorTipo cuListTipo, IListarTodas cuListAll, IWebHostEnvironment wheParam, IListarTiposCabañas cuListarTipos,
        IObtenerValorParam cuParam, IBuscarCabañaPorid cuBuscarCabaId , IListarPorMonto cuTipoMonto, IGetUsuarioByEmail cuUsuarioEmail, IEliminarCabaña cU_EliminarCabaña, IListarEnRangoFechas cU_ListarDisponiblesEnRango, IEditarCabaña EditarCabaña, IBuscarTipoPorId cU_BuscarTipoPorId
            , IHabilitarCabaña cU_HabilitarCabaña, IDeshabilitarCabaña cU_DeshabilitarCabaña)
        {
            CU_AltaCabaña = cuAlta;
            CU_EliminarCabaña = cU_EliminarCabaña;
            CU_ListarPorCantPersonas = cuListPers;
            CU_ListarPorHabilitadas = cuListHab;
            CU_ListarPorTexto = cuLisTxt;
            CU_ListarPorTipo = cuListTipo;
            CU_ListarTodas = cuListAll;
            WHE = wheParam;
            CU_ListarTiposCabañas = cuListarTipos;
            CU_ObtenerValorParametro = cuParam;
            CU_buscarCabañaPorId = cuBuscarCabaId;
            CU_ListarPorTipoYMonto = cuTipoMonto;
            CU_ObtenerUsuarioPorEmail = cuUsuarioEmail;
            CU_ListarDisponiblesEnRango = cU_ListarDisponiblesEnRango;
            CU_EditarCabaña = EditarCabaña;
            CU_BuscarTipoPorId = cU_BuscarTipoPorId;
            CU_HabilitarCabaña = cU_HabilitarCabaña;
            CU_DeshabilitarCabaña = cU_DeshabilitarCabaña;

            DescripcionCabaña.CantMinCarDescripcionCabaña = int.Parse(CU_ObtenerValorParametro.ValorParametro("CantMinCarDescripcionCabaña"));
            DescripcionCabaña.CantMaxCarDescripcionCabaña = int.Parse(CU_ObtenerValorParametro.ValorParametro("CantMaxCarDescripcionCabaña"));
        }




        #region DOCUMENTACION API
        /// <summary>
        /// Get all the Cabins
        /// </summary>
        /// <returns> Returns 404 Not Found if there aren't any Cabin in the system
        /// Returns 500 for errors in the server or database </returns>
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<CabañaDTO> cabañas = CU_ListarTodas.ListarTodas();
                if (cabañas.Any())
                {
                    return Ok(cabañas);
                }
                else
                {
                    return NotFound("There are no cabins in the system");
                }
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }

        }

        #region DOCUMENTACION API
        /// <summary>
        /// Get a Cabin for Id
        /// </summary>
        /// <param name="Id"> Id of the cabin to search </param>
        /// <returns>Return 400 Bad Request: If the number Id is less than zero
        /// 404 Not Found if there isn't any Cabin with that Id in the system
        /// Returns 500 for errors in the server or database
        /// 200 OK for success </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("{id}" , Name ="RutaDetail")]
        public IActionResult Get(int id) //DETAILS
        {
            try
            {
                if (id <= 0) return BadRequest("The cabin id to search must be a number greater than zero");
                CabañaDTO buscada = CU_buscarCabañaPorId.buscarPorId(id);
                if (buscada == null) return NotFound($"The Cabin with Id: {id} doesn't exist in the system");
                return Ok(buscada);
            }
            catch 
            {
                return StatusCode(500, "An unexpected error occurred");
            }

        }

        #region API DOCUMENTATION
        /// <summary>
        /// Create Cabin
        /// </summary>
        /// <param name="cabañadto"> Object Cabin to create in json format</param>
        /// <returns>Returns 400 Bad Request: If There is already a cabin with that name in the system- If the json object is not valid or the object to be created does not comply with the business rules (example: invalid Type Of Cabin)
        ///  Returns 500 for errors in the server or database 
        /// 200 OK for success</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CabañaDTO? cabañadto)// CREATE
        {
            if (cabañadto == null) return BadRequest("The cabin to create cannot be null");
            try
            {
                CU_AltaCabaña.AltaCabaña(cabañadto);
                return CreatedAtRoute("RutaDetail", new { id = cabañadto.NumeroHabitacion }, cabañadto);
            }
            catch (ExcepcionesCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(500,"An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Update Cabin
        /// </summary>
        /// <param name="cabañadto">Cabin object to create in json format </param>
        /// <param name="email">Email of the user who is updating the cabin  </param>
        /// <returns>Returns 400 Bad Request: If the object to be updated does not comply with the business rules (example: invalid Type Of Cabin) or the email is null or the user with that email is not the owner of the cabin
        ///  Returns 500 for errors in the server or database 
        ///  Returns 200 OK for success</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPut("edit/{email}")]
        [Authorize]
        public IActionResult Put([FromBody] CabañaDTO? cabañaDto, string email)
        {
            if (cabañaDto == null|| string.IsNullOrEmpty(email)) return BadRequest("To edit a Cabin you need provide the Cabin and the email of the user that is doing the changes");
            try
            {
                    CU_EditarCabaña.edit(cabañaDto,email);
                    return Ok();
            }
            catch (ExcepcionesTipoCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                return StatusCode(500, "Error connecting to database");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        #region DOCUMENTACION API
        /// <summary>
        /// Delete Cabin
        /// </summary>
        /// <param name="emailDueño">Email of the user who is deleting the cabin </param>
        /// <param name="idCabañaABorraar"> Id of the cabin to delete </param>
        /// <returns>Returns 400 Bad Request: If the email or the Id are null - If the cabin with that id does not exists - If the user with that email does not exists - If the user does not have the necessary permissions to delete the cabin
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 OK for success</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpDelete("{emailDueño}+{idCabañaABorraar}")]
        [Authorize]
        public IActionResult Delete(string emailDueño, int idCabañaABorraar)
        {
            if (string.IsNullOrEmpty(emailDueño) || idCabañaABorraar <= 0) return BadRequest("Se debe proporcionar una cabaña y un dueño para borrar");
            try
            {
                CabañaDTO buscada = CU_buscarCabañaPorId.buscarPorId(idCabañaABorraar);
                if (buscada == null)
                {
                    return BadRequest("La cabaña con id: " + idCabañaABorraar + " no existe en el sistema.");
                }
                UsuarioDTO dueño = CU_ObtenerUsuarioPorEmail.GetUsuarioByEmail(emailDueño);
                if (dueño == null)
                {
                    return BadRequest("El usuario con email: " + emailDueño + " no existe.");
                }
                    try
                    {
                        CU_EliminarCabaña.EliminarCabaña(emailDueño, idCabañaABorraar);
                        return Ok();
                    }
                    catch (ExcepcionesCabaña ex)
                    {
                        return BadRequest("No se pudo eliminar la cabaña Error: " + ex.Message);
                    }
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Get Cabins by text in name
        /// </summary>
        /// <param name="texto"> Text to search in the name of the cabins </param>
        /// <returns>Returns 400 Bad Request: If the text is null 
        /// Returns 404 Not Found: If no cabin has that text in the name
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("texto/{texto}")]
        public IActionResult GETBuscarPorTextoNombre(string texto) //LISTAR POR TEXTO EN EL NOMBRE
        {
            if (string.IsNullOrEmpty(texto)) return BadRequest("The name cannot be null");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTexto.ListarPorTexto(texto.ToLower());
                if (!cabañasdtos.Any()) return NotFound("There is no cabin with that name in the system");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Get Cabins by number of guests
        /// </summary>
        /// <param name="numero"> The number of guests to search </param>
        /// <returns>Returns 400 Bad Request: If the number is equals or less than zero
        /// Returns 404 Not Found: If is no cabin that has that capacity of guest
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("cantidadPersonas/{numero}")]
        public IActionResult GETBuscarPorCantPersonas(int numero)
        {
            if (numero <= 0) return BadRequest("The number provided must be a number bigger than zero");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorCantPersonas.ListarPorCantPersonas(numero);
                if (!cabañasdtos.Any()) return NotFound("There is no cabin that has that capacity for guests.");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }



        #region DOCUMENTACION API
        /// <summary>
        /// Get Cabins by Type Of Cabin
        /// </summary>
        /// <param name="idTipo"> The id of the Type Of Cabin </param>
        /// <returns>Returns 400 Bad Request: If the Id is equals or less than zero
        /// Returns 404 Not Found: If not exist any cabin of this Type Of Cabin
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("tipo/{idTipo}")]
        public IActionResult GETBuscarPorTipo(int idTipo)
        {
            if (idTipo <= 0) return BadRequest("Type Of Cabin cannot be null");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTipo.ListarPorTipo(idTipo);
                if (!cabañasdtos.Any()) return NotFound("There is no cabin with that type of in the system");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }



        #region DOCUMENTACION API
        /// <summary>
        /// Get disabled Cabins
        /// </summary>
        /// Returns 404 Not Found: If there is no cabin that is disabled
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("NOHabilitadas")]
        public IActionResult GETListarNOHabilitadas()
        {
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorHabilitadas.ListarCabañasHabilitadas();
                if (!cabañasdtos.Any()) return NotFound("There is no cabin that is disabled");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        #region DOCUMENTACION API
        /// <summary>
        /// Get Cabins by Daily Price
        /// </summary>
        /// <param name="monto"> The Daily Price to search the cabins</param>
        /// <returns>Returns 400 Bad Request: If The daily price to search is less than zero
        /// Returns 404 Not Found: if there is no cabin with a daily price equal to or less than the indicated one
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("Monto/{monto}")]
        public IActionResult GETListarPorMonto( int monto)
        {
            try
            {
                if (monto <= 0) return BadRequest("The daily price to search cannot be less than zero");
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTipoYMonto.ListarCabañasPorMonto(monto);
                if (!cabañasdtos.Any()) return NotFound("There is no cabin with a daily price lower than or equal to the one searched for. ");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Get cabins available to Rentalin a range of dates
        /// </summary>
        /// <param name="desde"> Start date</param>
        /// <param name="hasta"> End date</param>
        /// <returns>Returns 400 Bad Request: If The Start Date is bigger than the End Date
        /// Returns 404 Not Found: If not exists any cabin available to Rentalin those dates
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion

        [HttpGet("desde/{desde}/hasta{hasta}")]
        public IActionResult GETListarHabilitadasEnRangoFechas(DateTime desde,DateTime hasta)
        {
            try
            {
                if (desde>hasta ) return BadRequest("The start date cannot be greater than the end date");
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarDisponiblesEnRango.ListarEnRangoFechas(desde,hasta);
                if (!cabañasdtos.Any()) return NotFound("There are no cabins available to Rentalin that date range");
                return Ok(cabañasdtos);
            }
            catch (ExcepcionesCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                return StatusCode(500, "Error connecting to database");
            }
            catch (Exception ex )
            {
                return StatusCode(500, "An unexpected error occurred" + ex.Message);
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Enable Cabin
        /// </summary>
        /// <param name="email"> Email of the user that is trying to Enable the Cabin</param>
        /// <param name="idCabaña"> Id of the Cabin</param>
        /// <returns>Returns 400 Bad Request: If the email is null or the Id is less than zero - If the user with that email is not an administrator
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("habilitar/{email}/{idCabaña}")]
        [Authorize]
        public IActionResult HabilitarCabaña(string email, int idCabaña)
        {
            try
            {
                if (email == null || idCabaña <0) return BadRequest("A valid email and cabin must be provided");
                CU_HabilitarCabaña.HabiliarCabaña(email, idCabaña);
                return Ok();
            }
            catch (ExcepcionesCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                return StatusCode(500, "Error connecting to database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred" + ex.Message);
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Disable Cabin
        /// </summary>
        /// <param name="email"> Email of the user that is trying to Disable the Cabin</param>
        /// <param name="idCabaña"> Id of the Cabin</param>
        /// <returns>Returns 400 Bad Request: If the email is null or the Id is less than zero - If the user with that email is not an administrator
        ///  Returns 500 for errors in the server or database 
        /// Returns 200 succes with the list of cabins</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("deshabilitar/{email}/{idCabaña}")]
        [Authorize]
        public IActionResult DeshabilitarCabaña(string email, int idCabaña)
        {
            try
            {
                if (email == null || idCabaña < 0) return BadRequest("A valid email and cabin must be provided");
                CU_DeshabilitarCabaña.DeshabilitarCabaña(email, idCabaña);
                return Ok();
            }
            catch (ExcepcionesCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                return StatusCode(500, "Error connecting to database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred" + ex.Message);
            }
        }




    }
}
