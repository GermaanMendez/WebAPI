using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Mantenimiento.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        IAltaMantenimiento CU_AltaMantenimiento { get; set; }
        IListarMantenimientoPorCabaña CU_ListarPorCabaña { get; set; }
        IListarMantenimientoPorCabañaYFecha CU_ListarPorCabañaYFecha { get; set; }
        IObtenerMantenimientos CU_ObtenerMantenimientos { get; set; }
        IBuscarMantenimientoPorId CU_BuscarPorId { get; set; }
        IObtenerMantenimientosPorValores CU_ObtenerMantenimientoPorValores { get; set; }
        IGetUsuarioByEmail CU_GetUserByEmail { get; set; }  
        public MantenimientoController(IAltaMantenimiento cualta, IListarMantenimientoPorCabaña cuListPorCabaña, IListarMantenimientoPorCabañaYFecha cuListfecha, IObtenerMantenimientos cuListAll, IBuscarMantenimientoPorId cuBuscarPorid, IObtenerMantenimientosPorValores cU_ObtenerMantenimientoPorValores, IGetUsuarioByEmail cuGetUserById)
        {
            CU_AltaMantenimiento = cualta;
            CU_ListarPorCabañaYFecha = cuListfecha;
            CU_ListarPorCabaña = cuListPorCabaña;
            CU_ObtenerMantenimientos = cuListAll;
            CU_BuscarPorId = cuBuscarPorid;
            CU_ObtenerMantenimientoPorValores = cU_ObtenerMantenimientoPorValores;
            CU_GetUserByEmail = cuGetUserById;
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Obbtiene todos los mantenmientos en el sistema
        /// </summary>
        /// <returns>Retornará 404 Not Found: Si no hay ninguno en el sistema
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo salio bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<MantenimientoDTO> mantenimientosDTO = CU_ObtenerMantenimientos.ListarTodos();
                if (!mantenimientosDTO.Any()) return NotFound("There isn't any maintenance on the system");
                return Ok(mantenimientosDTO);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Obtiene un mantenimiento por su id
        /// </summary>
        /// <param name="id"> Id del mantenimiento a buscar </param>
        /// <returns>Retornará 400 Bad Request: Si el id es menor igual a 0
        /// 404 Not Found si no existe un mantenimiento con ese id buscado
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("{id}", Name = "RutaMantDetail")]
        public IActionResult Get(int id) //DETAILS
        {
            try
            {
                if (id <= 0) return BadRequest("The maintenance id to search for must be a number greater than zero.");
                MantenimientoDTO mantenimientoDTO= CU_BuscarPorId.BuscarPorId(id);
                if (mantenimientoDTO == null) return NotFound($"The Maintenance with id: {id} does not exists in the System.");
                return Ok(mantenimientoDTO);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }

        }

        #region DOCUMENTACION API
        /// <summary>
        /// Crea un Mantenimiento
        /// </summary>
        /// <param name="mantenimientodto"> Objeto Mantenimiento  a crear en formato json </param>
        /// <returns>Retornará 400 Bad Request: si el mantenimiento a crear es nulo o no se cumple con las reglas de negocio (ej: costo mayor a 0)
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("{email}")]
      //  [Authorize]
        public IActionResult Post([FromBody] MantenimientoDTO? mantenimientodto,string email)// CREATE
        {
            if (mantenimientodto == null || string.IsNullOrEmpty(email)) return BadRequest("You must provide maintenance to create");
            try
            {
                UsuarioDTO user = CU_GetUserByEmail.GetUsuarioByEmail(email);
                if (user == null) { return BadRequest("The user with this Email doesn't exists in the system"); }
                CU_AltaMantenimiento.AltaMantenimiento(mantenimientodto,email);
                return CreatedAtRoute("RutaMantDetail", new { id = mantenimientodto.Id }, mantenimientodto);
            }
            catch (ExcepcionesMantenimiento ex)
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
        /// Obtiene todos los mantenimientos realizados para una cabaña
        /// </summary>
        /// <param name="NumeroHabitacion"> Id de la cabaña  </param>
        /// <returns>Retornará 400 Bad Request: Si el id es menor igual a 0
        /// 404 Not Found si no existe ningun mantenimiento para esa cabaña
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("Cabaña/{NumeroHabitacion}")]
        public IActionResult GETListarPorCabaña(int NumeroHabitacion)
        {
            if (NumeroHabitacion <= 0) return BadRequest("To obtain the list of maintenance of the cabin you must provide the Id of the cabin");
            try
            {
                IEnumerable<MantenimientoDTO> mantenimientosDtos = CU_ListarPorCabaña.ListarPorCabaña(NumeroHabitacion);
                if (!mantenimientosDtos.Any()) return NotFound("There is no maintenance carried out on the selected cabin");
                return Ok(mantenimientosDtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        #region DOCUMENTACION API
        /// <summary>
        /// Obtiene los mantenimientos realizados a una cabaña filtrados por fecha
        /// </summary>
        /// <param name="Id"> Id de la cabaña </param>
        /// <param name="fecha1"> Fecha minima </param>
        /// <param name="fecha2"> Fecha maxima </param>
        /// <returns>Retornará 400 Bad Request: Si el id es menor igual a 0 o si las fechas no son validas
        /// 404 Not Found si no existe ningun mantenimiento para esa cabaña en ese rango de fechas
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion

        [HttpGet("{Id}/{fecha1}/{fecha2}")]
        public IActionResult GETListarMantenimientosPorFecha(int Id, DateTime fecha1, DateTime fecha2)
        {
            if (Id <= 0) return BadRequest("To obtain the list of maintenance of the cabin you must provide the Id of the cabin");
            if (fecha1 > fecha2) return BadRequest("The date from must be less than the until date");
            try
            {
                IEnumerable<MantenimientoDTO> mantenimientosDtos = CU_ListarPorCabañaYFecha.ListarPorCabañaYFecha(Id,fecha1,fecha2);
                if (!mantenimientosDtos.Any()) return NotFound("No maintenance has been performed on the selected cabin in that date range");
                return Ok(mantenimientosDtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Obtiene los mantenimientos realizados a cabañas filtrados por la capacidad de las cabañas
        /// </summary>
        /// <param name="valor1"> Valor minimo</param>
        /// <param name="valor2"> Valor maximo </param>
        /// <returns>Retornará 400 Bad Request: Si los valores ingresados no son validos
        /// 404 Not Found si no existe ningun mantenimiento para ese rango de valores
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si todo sale bien
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet("valor1={valor1}&valor2={valor2}")]
        public IActionResult GETListarMantenimientosPorValores (double valor1, double valor2)
        {
            if (valor1<0 || valor2<=0 || valor1>valor2) return BadRequest("Valor1 y Valor2 no pueden ser nulos ademas el valor 1 debe ser inferior al valor 2");
            try
            {
                IEnumerable<MantenimientoDTO> mantenimientosDtos = CU_ObtenerMantenimientoPorValores.ListarTrabajoEmpleadoPorValores(valor1, valor2);
                if (!mantenimientosDtos.Any()) return NotFound("No hay ningun mantenimiento en ese rango de  valores");
                return Ok(mantenimientosDtos);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }




    }
}
