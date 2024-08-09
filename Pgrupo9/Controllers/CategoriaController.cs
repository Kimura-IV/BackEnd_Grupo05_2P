using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DTO;
using Pgrupo9.API.Utilidad;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pgrupo9.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _categoriaServicio;

        public CategoriaController(ICategoriaService categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();

            try
            {
                rsp.status = true;
                rsp.Value = await _categoriaServicio.Lista();
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
