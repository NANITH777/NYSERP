using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class CCRepository : Repository<CostCenterAna>, ICCRepository
    {
        private ApplicationDbContext _db;
        public CCRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CostCenterAna obj)
        {
            _db.CostCenterAnas.Update(obj);
        }
    }
}
