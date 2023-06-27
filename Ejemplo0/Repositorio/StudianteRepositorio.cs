using Ejemplo0.Datos;
using Ejemplo0.Modelo;
using Ejemplo0.Repositorio.IRepository;

namespace Ejemplo0.Repositorio
{
    public class StudianteRepositorio : Repositorio<Studiante>, IStudiantes
    {
        private readonly SchoollContext _db;
        public StudianteRepositorio(SchoollContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Studiante> Update(Studiante entity)
        {
            _db.Studiantes.Update(entity);
            await _db.SaveChangesAsync();
            await Save();
            return entity;
        }
    }
}
