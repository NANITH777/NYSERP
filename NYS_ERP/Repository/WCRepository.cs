using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class WCRepository : Repository<WorkCenterAna>, IWCRepository
    {
        private ApplicationDbContext _db;
        public WCRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(WorkCenterAna obj)
        {
            _db.WorkCenterAnas.Update(obj);
        }
    }
}
