using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DTO;
using Pgrupo9.API.Utilidad;

namespace Pgrupo9.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {

        private readonly IDashBoardService _dashBoardServicio;

        public DashBoardController(IDashBoardService dashBoardServicio)
        {
            _dashBoardServicio = dashBoardServicio;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashBoardDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _dashBoardServicio.Resumen();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
