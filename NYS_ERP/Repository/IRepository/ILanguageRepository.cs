using NYS_ERP.Models;

namespace NYS_ERP.Repository.IRepository
{
    public interface ILanguageRepository : IRepository<Language>
    {
        void Update(Language obj);
    }
}
