using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_TipoCabaña.CasosUso;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ExepcionesPropias;
using DTOS;
using ExcepcionesPropias;
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
        public IListarAlquileresRealizadosPorUsuario CU_ListarAlquileresRealizadosUsuario { get; set; }

        public AlquilerCabañaController(IAltaAlquilerCabaña cuAlta, IEliminarAlquilerCabaña cuBaja, IEditarAlquilerCabaña cuEditar, IListarAlquileresDeMiCabañaDueño cuListarDueño, IListarAlquileresRealizadosPorUsuario cuListarAlquileresUsuario, IBuscarAlquilerPorId cuBuscarAlqId)
        {
            CU_AltaAlquilerCabaña = cuAlta;
            CU_EliminarAlquilerCabaña = cuBaja;
            CU_EditarAlquilerCabaña = cuEditar;
            CU_ListarAlquileresDeMiCabañaDueño = cuListarDueño;
            CU_ListarAlquileresRealizadosUsuario = cuListarAlquileresUsuario;
            CU_BuscarAlquilerPorId = cuBuscarAlqId;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AlquilerCabañaNuevoDTO? alquilerCabañaDto)
        {
            if (alquilerCabañaDto == null) return BadRequest("No se puede crear un alquiler vacia");
            try
            {
                CU_AltaAlquilerCabaña.AltaAlquilerCabaña(alquilerCabañaDto);
                return Ok("Alquiler creado con exito");
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        [HttpDelete("{emailDueño}/{idAlquilerABorraar}")]
        //[Authorize]
        public IActionResult Delete(string emailDueño, int idAlquilerABorraar)
        {
            if (string.IsNullOrEmpty(emailDueño) || idAlquilerABorraar <= 0) return BadRequest("Se debe proporcionar un alquiler y un dueño para borrar");
            try
            {
                CU_EliminarAlquilerCabaña.EliminarAlquilerCabaña(idAlquilerABorraar, emailDueño);
                return Ok();
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("No se pudo eliminar el alquiler Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        [HttpGet("{id}", Name = "RutaDetailAlquiler")]
        public IActionResult Get(int id) //DETAILS
        {
            try
            {
                if (id <= 0) return BadRequest("El id de alquiler debe ser un entero positivo");
                AlquilerCabañaDTO buscado = CU_BuscarAlquilerPorId.ObtenerPorId(id);
                if (buscado == null) return NotFound($"El alquiler con el id: {id} no existe en el sistema");
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        [HttpGet("email/{emailUsuario}/cabaña/{idCabaña}")]
        public IActionResult GetListarAlquileresDeMiCabaña(string emailUsuario, int idCabaña)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario) || idCabaña < 1 || idCabaña == null) return BadRequest("Debe proporcionar un email y cabaña válidos");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresDeMiCabañaDueño.ListarAlquileresDeMiCabañaDueño(emailUsuario, idCabaña);
                if (!alquileres.Any()) return NotFound("No se encontró ningún alquiler para la cabaña seleccionada");
                return Ok(alquileres);
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        [HttpGet("alquileresUsuario/{emailUsuario}")]
        public IActionResult GetListarAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario)) return BadRequest("Debe proporcionar un email válido");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresRealizadosUsuario.ListarAlquileresRealizadosPorUsuario(emailUsuario);
                if (!alquileres.Any()) return NotFound("No se encontró ningún alquiler realizado por el usuario");
                return Ok(alquileres);
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }
 

    }
    }
