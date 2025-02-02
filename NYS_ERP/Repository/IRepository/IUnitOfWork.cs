﻿namespace NYS_ERP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        ILanguageRepository Language { get; }
        ICountryRepository Country { get; }
        ICityRepository City { get; }
        IUnitRepository Unit { get; }
        IMaterialTRepository MaterialType { get; }
        ICostCenterRepository CostCenter { get; }
        IBOMRepository BOM { get; }
        IRotaRepository Rota { get; }
        IWorkCenterRepository WorkCenter { get; }
        IOperationRepository Operation { get; }
        ICCRepository CostCenterAna { get; }
        IMaterialRepository Material {  get; }
        IWCRepository WorkCenterAna { get; }
        IBOMAnaRepository BOMAna { get; }
        IRotaAnaRepository RotaAna { get; }
        void Save();
    }
}
