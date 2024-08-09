using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Proyectogrupo9.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyectogrupo9.DAL.Repositorios.Contrato;
using Proyectogrupo9.DAL.Repositorios;

using Proyectogrupo9.Utility;
using Proyectogrupo9.BLL.Servicios.Contrato;
using Proyectogrupo9.BLL.Servicios;

namespace Proyectogrupo9.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Proyectogrupo9Context>(options =>{
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            
            });

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository,VentaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));


            services.AddScoped<IRolService,RolService>();
            services.AddScoped<IUsuarioService,UsuarioService>();
            services.AddScoped<ICategoriaService,CategoriaService>();
            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<IVentaService,VentaService>();
            services.AddScoped<IDashBoardService,DashBoardService>();
            services.AddScoped<IMenuService,MenuService>();
        }
    }
}
