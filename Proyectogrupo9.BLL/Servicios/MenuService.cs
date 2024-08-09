using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.DAL.Repositorios.Contrato;
using Proyectogrupo9.DTO;
using Proyectogrupo9.Model;

namespace Proyectogrupo9.BLL.Servicios
{
    public class MenuService : IMenuService
    {

        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IGenericRepository<MenuRol> _MenuRolRepositorio;
        private readonly IGenericRepository<Menu> _MenuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepositorio, 
            IGenericRepository<MenuRol> menuRolRepositorio, 
            IGenericRepository<Menu> menuRepositorio, 
            IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _MenuRolRepositorio = menuRolRepositorio;
            _MenuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _MenuRolRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _MenuRepositorio.Consultar();



            try {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenus = tbResultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            
            } catch
            {
                throw;

            }
        }
    }
}
