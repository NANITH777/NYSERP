using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private ApplicationDbContext _db;
        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Country obj)
        {
            _db.Countries.Update(obj);
        }
    }
}
