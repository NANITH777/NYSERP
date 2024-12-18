using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(City obj)
        {
            _db.Cities.Update(obj);
        }
    }
}
