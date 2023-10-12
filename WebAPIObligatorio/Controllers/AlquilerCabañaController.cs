using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_TipoCabaña.CasosUso;
using CasosUso.CU_Usuario.CUInterfaces;
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

        public AlquilerCabañaController(IAltaAlquilerCabaña cuAlta, IEliminarAlquilerCabaña cuBaja, IEditarAlquilerCabaña cuEditar, IListarAlquileresDeMiCabañaDueño cuListarDueño, IListarAlquileresRealizadosPorUsuario cuListarAlquileresUsuario, IBuscarAlquilerPorId cuBuscarAlqId)
        {
            CU_AltaAlquilerCabaña = cuAlta;
            CU_EliminarAlquilerCabaña = cuBaja;
            CU_EditarAlquilerCabaña = cuEditar;
            CU_ListarAlquileresDeMiCabañaDueño = cuListarDueño;
            CU_BuscarAlquilerPorId = cuBuscarAlqId;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AlquilerCabañaNuevoDTO? alquilerCabañaDto)
        {
            if (alquilerCabañaDto == null) return BadRequest("The rental to be created cannot be null");
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

        [HttpDelete("{emailDueño}/{idAlquilerABorraar}")]
        //[Authorize]
        public IActionResult Delete(string emailDueño, int idAlquilerABorraar)
        {
            if (string.IsNullOrEmpty(emailDueño) || idAlquilerABorraar <= 0) return BadRequest("A rent to be deleted and owner's email must be provided");
            try
            {
                CU_EliminarAlquilerCabaña.EliminarAlquilerCabaña(idAlquilerABorraar, emailDueño);
                return Ok();
            }
            catch (ExepcionesAlquileresCabaña ex)
            {
                return BadRequest("Could not remove rent. Error: " + ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        

        [HttpGet("{id}", Name = "RutaDetailAlquiler")]
        public IActionResult Get(int id) //DETAILS
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Id");
                AlquilerCabañaDTO buscado = CU_BuscarAlquilerPorId.ObtenerPorId(id);
                if (buscado == null) return NotFound($"The rental with ID: {id} does not exist");
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("email/{emailUsuario}/cabaña/{idCabaña}")]
        public IActionResult GetListarAlquileresDeMiCabaña(string emailUsuario, int idCabaña)
        {
            try
            {
                if (string.IsNullOrEmpty(emailUsuario) || idCabaña < 1 || idCabaña == null) return BadRequest("A valid email and Id of  cabin must be provided");

                IEnumerable<AlquilerCabañaDTO> alquileres = CU_ListarAlquileresDeMiCabañaDueño.ListarAlquileresDeMiCabañaDueño(emailUsuario, idCabaña);
                if (!alquileres.Any()) return NotFound("No rental was found for the selected cabin");
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
