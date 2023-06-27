using Ejemplo0.Modelo;

namespace Ejemplo0.Repositorio.IRepository
{
    public interface IStudiantes : IRepositorio<Studiante> 
    {
        Task<Studiante> Update (Studiante entity);
    }
}
