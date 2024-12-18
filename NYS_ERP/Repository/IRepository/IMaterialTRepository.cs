using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IMaterialTRepository : IRepository<MaterialType>
    {
        void Update(MaterialType obj);
    }
}
