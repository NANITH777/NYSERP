using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class BOMRepository : Repository<BOM>, IBOMRepository
    {
        private ApplicationDbContext _db;
        public BOMRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BOM obj)
        {
            _db.BOMs.Update(obj);
        }
    }
}
