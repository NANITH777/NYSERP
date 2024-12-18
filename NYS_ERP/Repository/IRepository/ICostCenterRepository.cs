using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ICostCenterRepository : IRepository<CostCenter>
    {
        void Update(CostCenter obj);
    }
}
