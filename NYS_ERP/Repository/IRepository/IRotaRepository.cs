using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface IRotaRepository : IRepository<Rota>
    {
        void Update(Rota obj);
    }
}
