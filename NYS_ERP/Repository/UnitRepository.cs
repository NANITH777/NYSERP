using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private ApplicationDbContext _db;
        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Unit obj)
        {
            _db.Units.Update(obj);
        }
    }
}
