using Students_Management_Api.Models;

namespace Students_Management_Api.Services.LectureServices
{
    public interface ILectureService
    {
        IEnumerable<Lecture> GetAllLectures();
        Lecture GetLectureById(int lectureId);
        Task AddLecture(Lecture lecture);
        Task UpdateLecture(int id, Lecture lecture);
        Task DeleteLecture(Lecture lecture);
    }
}
