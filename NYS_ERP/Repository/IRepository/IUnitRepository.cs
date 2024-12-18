using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IUnitRepository : IRepository<Unit>
    {
        void Update(Unit obj);
    }
}
