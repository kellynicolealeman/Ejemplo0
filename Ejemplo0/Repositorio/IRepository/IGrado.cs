using Ejemplo0.Modelo;

namespace Ejemplo0.Repositorio.IRepository
{
    public interface IGrado : IRepositorio<Grado>
    {
        Task<Grado> Update(Grado entity);
    }
}
