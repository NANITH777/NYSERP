using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IBOMRepository : IRepository<BOM>
    {
        void Update(BOM obj);
    }
}
