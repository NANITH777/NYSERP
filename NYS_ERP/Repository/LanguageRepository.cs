using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private ApplicationDbContext _db;
        public LanguageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Language obj)
        {
            _db.Languages.Update(obj);
        }
    }
}
