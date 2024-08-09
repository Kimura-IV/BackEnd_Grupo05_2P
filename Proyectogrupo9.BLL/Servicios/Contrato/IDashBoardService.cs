using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyectogrupo9.DTO;

namespace Proyectogrupo9.BLL.Servicios.Contrato
{
    public interface IDashBoardService
    {

        Task<DashBoardDTO> Resumen();
    }
}
