using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class RotaRepository : Repository<Rota>, IRotaRepository
    {
        private ApplicationDbContext _db;
        public RotaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Rota obj)
        {
            _db.Rotas.Update(obj);
        }
    }
}
