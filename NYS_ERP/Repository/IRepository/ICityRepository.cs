using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void Update(City obj);
    }
}
