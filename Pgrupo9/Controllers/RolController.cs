using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DTO;
using Pgrupo9.API.Utilidad;

namespace Pgrupo9.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolService _rolServicio;

        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<RolDTO>>();

            try {
                rsp.status = true;
                rsp.Value = await _rolServicio.Lista();
            }catch(Exception ex) { 
                rsp.status=false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
