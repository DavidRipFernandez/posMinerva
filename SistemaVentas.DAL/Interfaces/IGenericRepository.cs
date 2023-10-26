using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;   



namespace SistemaVentas.DAL.Interfaces
{
    //TEntity sera una clase por eso le estoy diciendo el where 
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task<TEntity> Obtener (Expression<Func<TEntity, bool>> filtro); //Es asincrono y devolvera una entidad, la misma entidad que estamos enviandole, toda esa funcion sera conocida como filtro
        Task<TEntity> Crear   (TEntity entidad);
        Task<bool>    Editar  (TEntity entidad);
        Task<bool>    Eliminar(TEntity entidad);
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null);
    }
}
