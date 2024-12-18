using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        private ApplicationDbContext _db;
        public OperationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Operation obj)
        {
            _db.Operations.Update(obj);
        }
    }
}
