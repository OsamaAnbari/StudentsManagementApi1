namespace Students_Management_Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllEntities();
        T GetEntityById(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(int id);
    }
}
