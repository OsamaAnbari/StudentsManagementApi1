using Students_Management_Api.Models;

namespace Students_Management_Api.Services.TeacherServices
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int teacherId);
        Task AddTeacher(Teacher teacher, string email);
        Task UpdateTeacher(int id, Teacher teacher);
        Task DeleteTeacher(Teacher teacher);
    }
}
