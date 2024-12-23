using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IBOMAnaRepository : IRepository<BOMAna>
    {
        void Update(BOMAna obj);
    }
}
