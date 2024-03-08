using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.StudentMessageService
{
    public class StudentMessageService : IStudentMessageService
    {
        private readonly IRepository<StudentMessages> _teacherRepository;

        public StudentMessageService(IRepository<StudentMessages> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public IEnumerable<StudentMessages> GetAllMessages()
        {
            return _teacherRepository.GetAllEntities();
        }

        public StudentMessages GetMessageById(int teacherId)
        {
            return _teacherRepository.GetEntityById(teacherId);
        }

        public void AddMessage(StudentMessages teacher)
        {
            _teacherRepository.AddEntity(teacher);
        }

        public void UpdateMessage(StudentMessages teacher)
        {
            _teacherRepository.UpdateEntity(teacher);
        }

        public void DeleteMessage(int teacherId)
        {
            _teacherRepository.DeleteEntity(teacherId);
        }
    }
}
