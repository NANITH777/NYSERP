using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ICCRepository : IRepository<CostCenterAna>
    {
        void Update(CostCenterAna obj);
    }
}
