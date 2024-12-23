using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class BOMAnaRepository : Repository<BOMAna>, IBOMAnaRepository
    {
        private ApplicationDbContext _db;
        public BOMAnaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BOMAna obj)
        {
            _db.BOMAnas.Update(obj);
        }
    }
}
