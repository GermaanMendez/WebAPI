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

        public TipoCabañaController(IActualizarTipoCabaña cuAct, IAltaTipoCabaña cuAlta, IBuscarPorNomre cuBPN, IEliminarTipoCabaña cuBaja, IListarTiposCabañas cuListar, IBuscarTipoPorId cuBuscarId)
        {
            CU_ActTipoCabaña = cuAct;
            CU_AltaTipo = cuAlta;
            CU_BuscarPorNombre = cuBPN;
            CU_EliminarTipo = cuBaja;
            CU_ListarTipos = cuListar;
            CU_BuscarPorId = cuBuscarId;

        }

        #region DOCUMENTACION API
        /// <summary>
        /// Trae todos los tipos de cabañas
        /// </summary>
        /// <returns>Retornará 404 Not Found si no hay ningun tipo de cabaña registrado
        /// 500 ante errores de servidor o base de datos y 200 OK si todo salio bien</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        // GET: api/<TipoCabañaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<TipoCabañaDTO> tiposDtos = CU_ListarTipos.ListarTodos();
                if (!tiposDtos.Any()) return NotFound("No hay ningun tipo de cabaña registrado en el sistema");
                return Ok(tiposDtos);
            }
            catch
            {
                return StatusCode(500, " Ocurrio un error inesperado");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Obtiene un tipo de cabaña por su id
        /// </summary>
        /// <param name="id"> Id del tipo de cabaña que se quiere buscar</param>
        /// <returns>Retornará 400 Bad Request: Si el id es no es valido (ej: menor a 0)
        /// 404 Not Found si no existe ningun tipo de cabaña con ese id
        /// 500 ante errores de servidor o base de datos</returns>
        /// 200 OK si se encontro correctamente el tipo de cabañá
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        #endregion
        // GET api/<TipoCabañaController>/5
        [HttpGet("{id}", Name ="RutaDetailTipo")]
        public IActionResult Get(int id) //DETAILS
        {
            if (id < 0) return BadRequest("El id del tipo a buscar debe ser un numero entero positivo");
            try
            {
                TipoCabañaDTO buscado = CU_BuscarPorId.BuscarPorId(id);
                if (buscado == null) return NotFound($"El tipo con el id: {id} no existe");
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500, " Ocurrio un error inesperado");
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Crea un Tipo de Cabaña
        /// </summary>
        /// <param name="tipoCabañaDto"> Objeto tipo de cabaña a crear en formato json </param>
        /// <returns>Retornará 404 Bad Request: Si el objeto a crear es nulo, si no se cumple con las reglas de negocio(ej: caracteres minimos desc)
        /// 500 ante errores de servidor o base de datos y 201 si se creó correctamente</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        // POST api/<TipoCabañaController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] TipoCabañaDTO? tipoCabañaDto)
        {
            if (tipoCabañaDto == null) return BadRequest("No se puede crear un tipo de cabaña nulo");
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
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Edita un Tipo de Cabaña
        /// </summary>
        /// <param name="id"> Id de tipo int correspondiente al id  del tipo de cabaña a editar </param>
        /// <param name="tipoCabañaDto"> Objeto tipo de cabaña a editar en formato json </param>
        /// <returns>Retornará 404 Bad Request: Si el id es  y el tipo no son validos o si no se cumplen con las reglas de negocio (ej: minimo de caracatertes en desc)
        /// 500 ante errores de servidor o base de datos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        // PUT api/<TipoCabañaController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] TipoCabañaDTO? tipoCabañaDto)
        {
            if (tipoCabañaDto == null || id!=tipoCabañaDto.Id) return BadRequest("Se debe ingresar un tipo de cabaña valido para poder realizar su edicion");
            try
            {
                CU_ActTipoCabaña.ActualizarTipoCabaña(tipoCabañaDto);
                return Ok();
            }
            catch (ExcepcionesTipoCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExcepcionesBaseDeDatos ex)
            {
                return StatusCode(500, "Error al conectarse a la base de datos");
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado" );
            }
        }

        #region DOCUMENTACION API
        /// <summary>
        /// Elimina un Tipo de Cabaña
        /// </summary>
        /// <param name="id"> Id de tipo int correspondiente al id  del tipo de cabaña a borrar </param>
        /// <returns>Retornará 404 Bad Request: Si el id es  menor igual a 0 o el tipo no puede ser eliminado por alguna regla de negocio(ej: es usado por una cabañá)
        /// 404 Not Found si el tipo de cabaña no existe, 500 error servidor, 204 no content si se borrro correctamente</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest("El id debe ser un numero entero positivo");
            try
            {
                TipoCabañaDTO tipoCabaDto = CU_BuscarPorId.BuscarPorId(id);
                if (tipoCabaDto == null) return NotFound("El tipo de cabaña que se quiere eliminar no existe en el sistema");
                CU_EliminarTipo.BajaTipoCabaña(id);
                return NoContent();
            }
            catch (ExcepcionesTipoCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(500,"Ocurrio un error inesperado");
            }
        }


        #region DOCUMENTACION API
        /// <summary>
        /// Busca un tipo de cabaña por nombre
        /// </summary>
        /// <param name="nombre"> nombre de tipo string correspondiente al nombre  del tipo de cabaña a buscar </param>
        /// <returns>Retornará 400 Bad Request: Si el nombre es nulo 
        /// 404 Not Found si no existe hay un tipo con ese nombre, 500 error servidor, 201 si todo salio bien</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpGet("Nombre/{nombre}")]
        public IActionResult GETBuscarPoroNombre(string nombre) //BUSCAR POR NOMBRE
        {
            if (string.IsNullOrEmpty(nombre)) return BadRequest("El nombre no puede ser nulo");
            try
            {
                TipoCabañaDTO tipoBuscado = CU_BuscarPorNombre.BuscarTipoPorNombre(nombre.ToLower());
                if (tipoBuscado == null) return NotFound("No hay ningun tipo de cabaña con ese nombre");
                return CreatedAtRoute("RutaDetailTipo", new { id = tipoBuscado.Id }, tipoBuscado);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }


        


    }
}
