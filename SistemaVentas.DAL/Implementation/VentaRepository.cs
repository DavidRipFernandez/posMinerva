using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.DAL.DBContext;
using SistemaVentas.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVentas.DAL.Implementation
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly NsoftContext _dbContext;

        //como estamos heredando tambien de Generic Repository, necesitamos pasarle el base(dbContext) 
        public VentaRepository(NsoftContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //primero insertamos el detalle de venta, por que nosotros con esta interacion cambiaremos el stock del producto
                    //iterar los producto y reducir el stock en nuestra tabla
                    foreach(DetalleVenta dv in entidad.DetalleVenta)
                    {
                        Producto productoEncontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        productoEncontrado.Stock = productoEncontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(productoEncontrado);

                    }
                    //guardando todos los cambios de las operaciones que se realizaron
                    await _dbContext.SaveChangesAsync();
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "venta").First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;
                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroVenta = numeroVenta;

                    await _dbContext.Venta.AddAsync(entidad);

                    await _dbContext.SaveChangesAsync();

                    ventaGenerada = entidad;

                    //todas las operaciones las hace de manera temporal, una vez que hace todas las operaciones llega al commit y realizar recien un registro
                    transaction.Commit();

                }catch(Exception e){
                    transaction.Rollback();
                }

            }
            return ventaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)

        {
            //include sirve como un join entre las tablas, esta funcionando para el detalle de Venta
            //ThenInclude es para que regrese a detalle de venta
            //.Include() es para que regrese al detalle de venta 
            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta.Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listaResumen;
        }
    }
}
