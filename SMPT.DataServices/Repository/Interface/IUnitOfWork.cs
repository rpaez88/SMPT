namespace SMPT.DataServices.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //IAreaRepository Areas { get; }
        //ICareerRepository Careers { get; }
        //ICycleRepository Cycles { get; }
        //IEvidenceRepository Evidences { get; }
        //IEvidenceStateRepository EvidenceStates { get; }
        //IRoleRepository Roles { get; }
        //IStudentRepository Students { get; }
        //IStudentStateRepository StudentStates { get; }
        //IUserRepository Users { get; }

        Task CompleteAsync();

        IRepository<T> GetRepository<T>() where T : class;
        void SaveChanges();
    }
}
