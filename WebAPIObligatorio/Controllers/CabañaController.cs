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
        IListarPorHabilitadas CU_ListarPorHabilitadas { get; set; }
        IListarPorTexto CU_ListarPorTexto { get; set; }
        IListarPorTipo CU_ListarPorTipo { get; set; }
        IListarTodas CU_ListarTodas { get; set; }
        IListarPorMonto CU_ListarPorTipoYMonto { get; set; }
        IBuscarCabañaPorid CU_buscarCabañaPorId { get; set; }

        IListarTiposCabañas CU_ListarTiposCabañas { get; set; }
        IListarEnRangoFechas CU_ListarDisponiblesEnRango { get; set; }
        IObtenerValorParam CU_ObtenerValorParametro { get; set; }
        IGetUsuarioByEmail CU_ObtenerUsuarioPorEmail { get; set; }
        public IWebHostEnvironment WHE { get; set; }
        public CabañaController(IAltaCabaña cuAlta, IListarPorCantPersonas cuListPers, IListarPorHabilitadas cuListHab, IListarPorTexto cuLisTxt, IListarPorTipo cuListTipo, IListarTodas cuListAll, IWebHostEnvironment wheParam, IListarTiposCabañas cuListarTipos,
        IObtenerValorParam cuParam, IBuscarCabañaPorid cuBuscarCabaId , IListarPorMonto cuTipoMonto, IGetUsuarioByEmail cuUsuarioEmail, IEliminarCabaña cU_EliminarCabaña, IListarEnRangoFechas cU_ListarDisponiblesEnRango)
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
                    return NotFound("No hay cabañas en el sistema");
                }
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
                if (id <= 0) return BadRequest("El id de cabaña debe ser un entero positivo");
                CabañaDTO buscada = CU_buscarCabañaPorId.buscarPorId(id);
                if (buscada == null) return NotFound($"La cabaña con el id: {id} no existe en el sistema");
                return Ok(buscada);
            }
            catch 
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
        public IActionResult Post([FromBody] CabañaNuevaDTO ? cabañadto)// CREATE
        {
            if (cabañadto == null) return BadRequest("No se puede crear una cabaña vacia");
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
                return StatusCode(500,"Ocurrio un error inesperado");
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
                Usuario dueño = CU_ObtenerUsuarioPorEmail.GetUsuarioByEmail(emailDueño);
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
                return StatusCode(500, "Ocurrio un error inesperado");
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
            if (string.IsNullOrEmpty(texto)) return BadRequest("El texto no puede ser nulo");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTexto.ListarPorTexto(texto.ToLower());
                if (!cabañasdtos.Any()) return NotFound("No hay ninguna cabaña que su nombre tenga el texto buscado");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
            if (numero <= 0) return BadRequest("El numero a buscar debe ser un entero positivo");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorCantPersonas.ListarPorCantPersonas(numero);
                if (!cabañasdtos.Any()) return NotFound("No hay ninguna cabaña que aloje minimo esa cantidad de personas");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
            if (idTipo <= 0) return BadRequest("Se debe ingresar un tipo de cabaña a buscar");
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorTipo.ListarPorTipo(idTipo);
                if (!cabañasdtos.Any()) return NotFound("No hay ninguna cabaña de ese tipo");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
        [HttpGet("Habilitadas")]
        public IActionResult GETListarSoloHabilitadas()
        {
            try
            {
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarPorHabilitadas.ListarCabañasHabilitadas();
                if (!cabañasdtos.Any()) return NotFound("No hay ninguna cabaña habilitada");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
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
                if (!cabañasdtos.Any()) return NotFound("No hay ninguna cabaña que cumpla con los filtros seleccionado ");
                return Ok(cabañasdtos);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        [HttpGet("desde/{desde}/hasta{hasta}")]
        public IActionResult GETListarHabilitadasEnRangoFechas(DateTime desde,DateTime hasta)
        {
            try
            {
                if (desde == null || hasta == null) return BadRequest("Se deben proporcionar las fechas");
                IEnumerable<CabañaDTO> cabañasdtos = CU_ListarDisponiblesEnRango.ListarEnRangoFechas(desde,hasta);
                if (!cabañasdtos.Any()) return NotFound("No hay cabañas disponibles para alquilar en ese rango de fechas");
                return Ok(cabañasdtos);
            }
            catch(Exception ex )
            {
                return StatusCode(500, "Ocurrio un error inesperado" + ex.Message);
            }
        }


    }
}
