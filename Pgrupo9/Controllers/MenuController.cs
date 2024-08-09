using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DTO;
using Pgrupo9.API.Utilidad;

namespace Pgrupo9.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuServicio;

        public MenuController(IMenuService menuServicio)
        {
            _menuServicio = menuServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _menuServicio.Lista(idUsuario);
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
