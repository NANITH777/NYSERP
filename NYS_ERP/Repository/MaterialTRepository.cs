using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class MaterialTRepository : Repository<MaterialType>, IMaterialTRepository
    {
        private ApplicationDbContext _db;
        public MaterialTRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MaterialType obj)
        {
            _db.MaterialTypes.Update(obj);
        }
    }
}
