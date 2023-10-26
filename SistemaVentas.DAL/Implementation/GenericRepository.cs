using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace SistemaVentas.DAL.Implementation
{
    //Aqui esta obteniendo la herencia de iGeneric
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //constructor para empezar creando el contexto de la base de datos
        private readonly NsoftContext _nsoftContext;
        public GenericRepository(NsoftContext nsoftContext)
        {
            _nsoftContext = nsoftContext;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _nsoftContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch 
            {
                throw;
            }
        }


        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                _nsoftContext.Set<TEntity>().Add(entidad);
                await _nsoftContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                _nsoftContext.Set<TEntity>().Update(entidad); //algo mas corto -> _nsoftContext.Update(entidad)
                await _nsoftContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                _nsoftContext.Remove(entidad);
                await _nsoftContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            // validar si el filtro es igual a null, en caso que lo sea tiene que devolver la consulta a este tabla que realizamos la consulta
            // en caso de que el filtro exista, entonces necesitamos hacer un select a ese tabla con ciertos filtros

            IQueryable<TEntity> queryEntidad = filtro == null ? _nsoftContext.Set<TEntity>() : _nsoftContext.Set<TEntity>().Where(filtro);
            return queryEntidad;
        
        }


    }
}
