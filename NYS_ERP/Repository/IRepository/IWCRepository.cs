using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IWCRepository : IRepository<WorkCenterAna>
    {
        void Update(WorkCenterAna obj);
    }
}
