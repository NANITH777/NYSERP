using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IOperationRepository : IRepository<Operation>
    {
        void Update(Operation obj);
    }
}
