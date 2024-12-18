using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class WorkCenterRepository : Repository<WorkCenter>, IWorkCenterRepository
    {
        private ApplicationDbContext _db;
        public WorkCenterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(WorkCenter obj)
        {
            _db.WorkCenters.Update(obj);
        }
    }
}
