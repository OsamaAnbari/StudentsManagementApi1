using Students_Management_Api.Models;

namespace Students_Management_Api.Services.ClassServices
{
    public interface IClassService
    {
        IEnumerable<Class> GetAllClasss();
        Class GetClassById(int @classId);
        void AddClass(Class @class);
        void UpdateClass(Class @class);
        void DeleteClass(int @classId);
    }
}
