using Students_Management_Api.Models;

namespace Students_Management_Api.Services.StudentServices
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int studentId);
        Task AddStudent(Student student, string email);
        Task UpdateStudent(int id, Student student);
        Task DeleteStudent(Student student);
    }
}
