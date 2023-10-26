using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVentas.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.DAL.Implementation;
using SistemaVentas.DAL.Interfaces;
//using SistemaVenta.BLL.Interfaces;
//using SistemaVenta.BLL.Implementation;

// se agregaran todas las dependecias que se usaran en este clase
namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        //todos los servicios iran mas adelante en este lado
        public static void InyectarDependecia(this IServiceCollection services, IConfiguration Configuration)
        {
            
            services.AddDbContext<NsoftContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            }
            );
            //Desta forma estariamos trabajando con la clase generica y la interfaz
            //como es generico no se con que clase o entidad estaré trabajando, es por eso que utilizo AddTrasient, es decir que varia sus valores.
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

        }
    }
}
