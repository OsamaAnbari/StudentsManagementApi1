using Microsoft.EntityFrameworkCore;

namespace Students_Management_Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAllEntities()
        {
            return _dbSet.ToList();
        }

        public T GetEntityById(int id)
        {
            return _dbSet.Find(id);
        }

        public void AddEntity(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateEntity(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
