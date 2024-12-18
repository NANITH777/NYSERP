using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country obj);
    }
}
