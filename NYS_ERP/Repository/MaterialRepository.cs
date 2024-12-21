using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private ApplicationDbContext _db;

        public MaterialRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Material obj)
        {
            _db.Materials.Update(obj);
        }
    }
}
