using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}
