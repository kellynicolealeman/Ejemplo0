using Ejemplo0.Datos;
using Ejemplo0.Modelo;
using Ejemplo0.Repositorio.IRepository;

namespace Ejemplo0.Repositorio
{
    public class GradoRepositorio : Repositorio<Grado>, IGrado
    {
        private readonly SchoollContext _db;
        public GradoRepositorio(SchoollContext db) : base(db)
        {
            _db = db;

        }

        public async Task<Grado> Update(Grado entity)
        {
            _db.Grados.Update(entity);
            await _db.SaveChangesAsync();
            await Save();
            return entity;
        }
    }
}
