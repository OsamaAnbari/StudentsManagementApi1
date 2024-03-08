using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.ClassServices
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _classRepository;

        public ClassService(IRepository<Class> @classRepository)
        {
            _classRepository = @classRepository;
        }

        public IEnumerable<Class> GetAllClasss()
        {
            return _classRepository.GetAllEntities();
        }

        public Class GetClassById(int @classId)
        {
            return _classRepository.GetEntityById(@classId);
        }

        public void AddClass(Class @class)
        {
            _classRepository.AddEntity(@class);
        }

        public void UpdateClass(Class @class)
        {
            _classRepository.UpdateEntity(@class);
        }

        public void DeleteClass(int @classId)
        {
            _classRepository.DeleteEntity(@classId);
        }
    }
}
