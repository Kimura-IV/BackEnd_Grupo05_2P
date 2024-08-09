using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DAL.Repositorios.Contrato;
using Proyectogrupo9.DTO;
using Proyectogrupo9.Model;

namespace Proyectogrupo9.BLL.Servicios
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DashBoardService(IVentaRepository ventaRepositorio, 
            IGenericRepository<Producto> productoRepositorio, 
            IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);


            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task <int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total; 
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaventa = retornarVentas(_ventaQuery, -7);

                resultado = tablaventa.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-EC"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();

            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string,int>resultado = new Dictionary<string,int>();

            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }

        public async Task<DashBoardDTO> Resumen()
        {
            DashBoardDTO vmdahsBoard = new DashBoardDTO();

            try {

                vmdahsBoard.TotalVentas = await TotalVentasUltimaSemana();
                vmdahsBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmdahsBoard.TotalProductos = await TotalProductos();


                List<VentasSemanaDTO> listaVentaSemana = new List<VentasSemanaDTO>();

                foreach (KeyValuePair<string,int> item in await VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentasSemanaDTO(){
                        Fecha=item.Key,
                        Total=item.Value,

                    });
                }

                vmdahsBoard.VentasUltimaSemana = listaVentaSemana;
                

            } catch {
                throw;
            }

            return vmdahsBoard;
        }
    }
}
