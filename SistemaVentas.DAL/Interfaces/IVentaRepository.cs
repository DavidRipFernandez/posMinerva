using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity;


namespace SistemaVentas.DAL.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        /*
         * Se declara un método llamado Registrar que toma un parámetro de tipo Venta llamado entidad. 
         * Este método devuelve una tarea asincrónica (Task) que eventualmente produce un resultado de tipo Venta.
         */
        Task<Venta> Registrar(Venta entidad);
        Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin);
    }
}
