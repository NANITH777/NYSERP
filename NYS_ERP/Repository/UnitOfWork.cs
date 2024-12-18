using NYS_ERP.Models;
using NYS_ERP.Repository.IRepository;

namespace NYS_ERP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICompanyRepository Company { get; private set; }
        public ILanguageRepository Language { get; private set; }
        public ICountryRepository Country { get; private set; }
        public ICityRepository City { get; private set; }
        public IUnitRepository Unit { get; private set; }
        public IMaterialTRepository MaterialType { get; private set; }
        public ICostCenterRepository CostCenter { get; private set; }
        public IBOMRepository BOM { get; private set; } 
        public IRotaRepository Rota { get; private set; }
        public IWorkCenterRepository WorkCenter { get; private set; }
        public IOperationRepository Operation { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Company = new CompanyRepository(_db);
            Language = new LanguageRepository(_db);
            Country = new CountryRepository(_db);
            City = new CityRepository(_db);
            Unit = new UnitRepository(_db);
            MaterialType = new MaterialTRepository(_db);
            CostCenter = new CostCenterRepository(_db);
            BOM = new BOMRepository(_db);
            Rota = new RotaRepository(_db);
            WorkCenter = new WorkCenterRepository(_db);
            Operation = new OperationRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
