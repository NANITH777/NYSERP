using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IWorkCenterRepository : IRepository<WorkCenter>
    {
        void Update(WorkCenter obj);
    }
}
