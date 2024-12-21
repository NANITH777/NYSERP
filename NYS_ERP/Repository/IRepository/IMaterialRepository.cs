using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IMaterialRepository : IRepository<Material>
    {
        void Update(Material obj);
    }
}
