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
        /// Get all the Maintenances
        /// </summary>
        /// <returns>Returns 404 Not Found: If not exists any Maintenance in the system
        /// Returns 500 for errors in the server or database 
        /// Returns 200 OK for success with the list of maintenances</returns>
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
        /// Get a Maintenance by Id
        /// </summary>
        /// <param name="id"> Id of the maintenance to search </param>
        /// <returns>Returns 400 Bad Request: If the id is equals or less than zero
        /// 404 Not Found if is not any maintenance with that Id
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success </returns>
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
        /// Create Maintenance
        /// </summary>
        /// <param name="mantenimientodto"> Maintenance Object to create in json format </param>
        /// <returns>Returns 400 Bad Request:if the maintenance to be created is null or the business rules are not met (e.g. cost greater than 0)
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost("{email}")]
        [Authorize]
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
        /// Gets all maintenance performed for a cabin
        /// </summary>
        /// <param name="NumeroHabitacion"> Cabin ID </param>
        /// <returns>Returns 400 Bad Request: If the id is less than 0
        /// 404 Not Found if there is no maintenance for that cabin
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success </returns>
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
        /// Obtains the maintenances carried out on a cabin filtered by date
        /// </summary>
        /// <param name="Id"> Id of the cabin </param>
        /// <param name="fecha1"> Minimum date </param>
        /// <param name="fecha2"> Maximum date </param>
        /// <returns>Returns 400 Bad Request: If the id is less than 0 or if the dates are not valid
        /// 404 Not Found if there is no maintenance for that cabin in that date range
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success </returns>
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
        /// Obtains the maintenances carried out on cabins filtered by the capacity of the cabins
        /// </summary>
        /// <param name="value1"> Minimum value</param>
        /// <param name="value2"> Maximum value </param>
        /// <returns>Returns 400 Bad Request: If the values entered are not valid
        /// 404 Not Found if there is no maintenance for that range of values
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success </returns>
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
