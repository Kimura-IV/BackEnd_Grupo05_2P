using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyectogrupo9.DTO;

namespace Proyectogrupo9.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<VentaDTO> Registrar(VentaDTO modelo);
        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechainicio, string fechafin);
        Task<List<ReporteDTO>> Reporte(string fechainicio, string fechafin);
    }
}
