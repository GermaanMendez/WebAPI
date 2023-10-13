using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_TipoCabaña.InterfacesCU;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using Dominio_Interfaces.EnitdadesNegocio;
using CasosUso.CU_Usuario.CUInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIObligatorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCabañaController : ControllerBase
    {
        public IActualizarTipoCabaña CU_ActTipoCabaña { get; set; }
        public IAltaTipoCabaña CU_AltaTipo { get; set; }
        public IBuscarPorNomre CU_BuscarPorNombre { get; set; }
        public IEliminarTipoCabaña CU_EliminarTipo { get; set; }
        public IListarTiposCabañas CU_ListarTipos { get; set; }
        public IBuscarTipoPorId CU_BuscarPorId { get; set; }
        public IGetUsuarioByEmail CU_GetUserByEmail { get; set; }

        public TipoCabañaController(IActualizarTipoCabaña cuAct, IAltaTipoCabaña cuAlta, IBuscarPorNomre cuBPN, IEliminarTipoCabaña cuBaja, IListarTiposCabañas cuListar, IBuscarTipoPorId cuBuscarId, IGetUsuarioByEmail cuGetUserEmail)
        {
            CU_ActTipoCabaña = cuAct;
            CU_AltaTipo = cuAlta;
            CU_BuscarPorNombre = cuBPN;
            CU_EliminarTipo = cuBaja;
            CU_ListarTipos = cuListar;
            CU_BuscarPorId = cuBuscarId;
            CU_GetUserByEmail=cuGetUserEmail;
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Get all types of cabins
        /// </summary>
        /// <returns>Returns 404 Not Found if not exists any Type Of Cabin in the sistem
        /// 500 for server or database errors and 200 OK if everything went well</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<TipoCabañaDTO> tiposDtos = CU_ListarTipos.ListarTodos();
                if (!tiposDtos.Any()) return NotFound("There is not any Type Of Cabin in the system");
                return Ok(tiposDtos);
            }
            catch
            {
                return StatusCode(500, " An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Gets Type of Cabin by Id
        /// </summary>
        /// <param name="id"> Id of the Type of Cabin to search </param>
        /// <returns>Returns 400 Bad Request: If the id is less than zero
        /// 404 Not Found if there is no type of cabin with that id
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK succes with the Type of Cabin
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        // GET api/<TipoCabañaController>/5
        [HttpGet("{id}", Name ="RutaDetailTipo")]
        public IActionResult Get(int id) //DETAILS
        {
            if (id < 0) return BadRequest("The Id of type to search must be a number bigger than zero");
            try
            {
                TipoCabañaDTO buscado = CU_BuscarPorId.BuscarPorId(id);
                if (buscado == null) return NotFound($"There is not any Type Of Cabin with the Id: {id}");
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500, " An unexpected error occurred");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Create a Type of Cabin
        /// </summary>
        /// <param name="tipoCabañaDto"> Type Of Cabin object to create in json format </param>
        /// <returns>Returns 404 Bad Request: If the object to be created is null, if the business rules are not met (example: characters minimum descripcion)
        /// Returns 500 for server or database errors
        /// Returns 200 Ok for success</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] TipoCabañaDTO? tipoCabañaDto)
        {
            if (tipoCabañaDto == null) return BadRequest("The Type Of Cabin to create cannot be null");
            try
            {
                CU_AltaTipo.AltaTipoCabaña(tipoCabañaDto);
                return CreatedAtRoute("RutaDetailTipo", new { id = tipoCabañaDto.Id }, tipoCabañaDto);
            }
            catch (ExcepcionesTipoCabaña ex)
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
        /// Edit a Type Of Cabin
        /// </summary>
        /// <param name="id"> Int type id corresponding to the id of the type of cabin to edit </param>
        /// <param name="tipoCabañaDto"> Type Of Cabin object to edit in json format </param>
        /// <param name="email">Email of the user who is trying to update the Type Of Cabin</param>
        /// <returns>Returns 404 Bad Request: If the Type Of Cabin does not exist in the system - If the user with that email is not an administrator
        /// Returns 500 for errors in the server or database
        /// Returns 200 OK for success</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion

        [HttpPut("{id}/{email}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] TipoCabañaDTO? tipoCabañaDto,string email)
        {
            if (tipoCabañaDto == null || id!=tipoCabañaDto.Id|| string.IsNullOrEmpty(email)) return BadRequest("To edit a Type of Cabin you need provide the id of Type Cabin and the email of the user that is doing the changes");
            try
            {
                UsuarioDTO user = CU_GetUserByEmail.GetUsuarioByEmail(email);
                if (user.Rol.ToLower() != "administrador")
                {
                    return BadRequest("The user " + email + " doesn't have the permissions to edit this");
                }
                else
                {
                   CU_ActTipoCabaña.ActualizarTipoCabaña(tipoCabañaDto);
                   return Ok();
                }
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
                return StatusCode(500, "An unexpected error occurred" );
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Delete a Type Of Cabin
        /// </summary>
        /// <param name="id"> Id of the Cabin to delete </param>
        /// <param name="email"> Email of the user who is trying to delete the Type Of Cabin</param>
        /// <returns>Returns 404 Bad Request: If the id is less than 0 - If the Type Of Cabin is being used by a cabin - If the user trying to delete the Type Of Cabin is not an administrator
        /// Returns 404 Not Found if the Type Of Cabin does not exist,
        /// Returns 500 server error,
        /// Returns 204 no content if deleted successfully</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpDelete("{id}/{email}")]
        [Authorize]
        public IActionResult Delete(int id,string email)
        {
            if (id < 0 || string.IsNullOrEmpty(email)) return BadRequest("To Delete a Type of Cabin you need provide the id of Type Cabin and the email of the user that is doing the changes");
            try
            {
                UsuarioDTO user = CU_GetUserByEmail.GetUsuarioByEmail(email);
                if (user.Rol.ToLower() != "administrador")
                {
                    return BadRequest("The user " + email + " doesn't have the permissions to delete this");
                }
                else
                {
                    TipoCabañaDTO tipoCabaDto = CU_BuscarPorId.BuscarPorId(id);
                    if (tipoCabaDto == null) return NotFound("The type of cabin you want to delete does not exist in the system");
                    CU_EliminarTipo.BajaTipoCabaña(id);
                    return NoContent();
                }
            }
            catch (ExcepcionesTipoCabaña ex)
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
        /// Search for a Type Of Cabin by name
        /// </summary>
        /// <param name="nombre"> Name of the Type Of Cabin to search </param>
        /// <returns>Returns 400 Bad Request: If name is null
        /// Returns 404 Not Found if there is no Type Of Cabin with that name
        /// Returns 500 server error
        /// Returns 201 with Type Of Cabin</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("Nombre/{nombre}")]
        public IActionResult GETBuscarPoroNombre(string nombre) //BUSCAR POR NOMBRE
        {
            if (string.IsNullOrEmpty(nombre)) return BadRequest("The name to search cannot be null");
            try
            {
                TipoCabañaDTO tipoBuscado = CU_BuscarPorNombre.BuscarTipoPorNombre(nombre.ToLower());
                if (tipoBuscado == null) return NotFound("There is not any Type Of Cabin with that name");
                return CreatedAtRoute("RutaDetailTipo", new { id = tipoBuscado.Id }, tipoBuscado);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }


        


    }
}
