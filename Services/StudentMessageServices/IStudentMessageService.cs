using Students_Management_Api.Models;

namespace Students_Management_Api.Services.StudentMessageService
{
    public interface IStudentMessageService
    {
        IEnumerable<StudentMessages> GetAllMessages();
        StudentMessages GetMessageById(int teacherId);
        void AddMessage(StudentMessages teacher);
        void UpdateMessage(StudentMessages teacher);
        void DeleteMessage(int teacherId);
    }
}
