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
        /// Obtiene todas las Cabañas
        /// </summary>
        /// <returns> 404 Not Found si no existe ninguna cabaña en el sistema 
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
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
        /// Busca una cabaña por su id
        /// </summary>
        /// <param name="Id"> Id de la cabaña a buscar </param>
        /// <returns>Retornará 400 Bad Request: Si el id es menor igual a 0
        /// 404 Not Found si no existe ninguna cabaña con ese id
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
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

        #region DOCUMENTACION API
        /// <summary>
        /// Crea una cabaña
        /// </summary>
        /// <param name="cabañadto"> Objecto cabaña a crear en formato json </param>
        /// <returns>Retornará 400 Bad Request: Si el objeto json no es valido o si el objeto a crear no cumple con las reglas de negocio (ej: tipo de cabaña invaldo)
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost]
        //[Authorize]
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
        [HttpPut("edit/{email}")]
        //[Authorize]
        public IActionResult Put([FromBody] CabañaDTO? cabañaDto, string email)
        {
            if (cabañaDto == null|| string.IsNullOrEmpty(email)) return BadRequest("To edit a Cabin you need provide the Cabin and the email of the user that is doing the changes");
            try
            {
                    CU_EditarCabaña.edit(cabañaDto);
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

        [HttpDelete("{emailDueño}+{idCabañaABorraar}")]
        //[Authorize]
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
        /// Retorna las cabañas que en su nombre contegan el texto ingresado
        /// </summary>
        /// <param name="texto"> Texto a buscar en el nombre </param>
        /// <returns>Retornará 400 Bad Request: Si el texto buscado es nulo
        /// 404 Not Found si no existe ninguna cabaña que contenga ese texto en el nombre
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Retorna las cabañas que tengan una capacidad mayor o igual a la ingresda
        /// </summary>
        /// <param name="numero"> Capacidad minima a buscar </param>
        /// <returns>Retornará 400 Bad Request: Si el numero es menor o igual a 0
        /// 404 Not Found si no existe ninguna cabaña con una capacidad mayor o igual a la ingresada
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Retorna las cabañas que sean del tipo de cabaña ingresdo
        /// </summary>
        /// <param name="idTipo"> Id del tipo de cabaña a buscar </param>
        /// <returns>Retornará 400 Bad Request: Si el Id de tipo es menor o igual a 0
        /// 404 Not Found si no existe ninguna cabaña de ese tipo
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("tipo/{idTipo}")]
        public IActionResult GETBuscarPorTipo(int idTipo)
        {
            if (idTipo <= 0) return BadRequest("Cabin type cannot be null");
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
        /// Retorna las cabañas que esten habilitadas
        /// </summary>
        /// <returns>Retornará  404 Not Found si no existe ninguna cabaña que este habilitada
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
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
        /// Retorna que tengan jacuzzi, que esten disponibles y ademas su costo diario(costo tipo multiplicado por capacidad de la cabaña) sea menor al ingresado
        /// </summary>
        /// <returns>Retornará  404 Not Found si no existe ninguna cabaña que cumpla con esos requerimientos
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion

        [HttpGet("Monto/{monto}")]
        public IActionResult GETListarPorMonto( int monto)
        {
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTipoYMonto.ListarCabañasPorMonto(monto);
                if (!cabañasdtos.Any()) return NotFound("There is no cabin with a daily price lower than or equal to the one searched for. ");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("desde/{desde}/hasta{hasta}")]
        public IActionResult GETListarHabilitadasEnRangoFechas(DateTime desde,DateTime hasta)
        {
            try
            {
                if (desde>hasta ) return BadRequest("The start date cannot be greater than the end date");
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarDisponiblesEnRango.ListarEnRangoFechas(desde,hasta);
                if (!cabañasdtos.Any()) return NotFound("There are no cabins available to rent in that date range");
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

        [HttpPost("habilitar/{email}/{idCabaña}")]
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

        [HttpPost("deshabilitar/{email}/{idCabaña}")]
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
