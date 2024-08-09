using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyectogrupo9.Model;

namespace Proyectogrupo9.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {

        Task<Venta> Registrar(Venta modelo);
    }
}
