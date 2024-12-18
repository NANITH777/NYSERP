using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class CostCenterRepository : Repository<CostCenter>, ICostCenterRepository
    {
        private ApplicationDbContext _db;
        public CostCenterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CostCenter obj)
        {
            _db.CostCenters.Update(obj);
        }
    }
}
