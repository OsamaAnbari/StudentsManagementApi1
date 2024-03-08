using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.LectureServices
{
    public class LectureService : ILectureService
    {
        private readonly IRepository<Lecture> _lectureRepository;

        public LectureService(IRepository<Lecture> lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public IEnumerable<Lecture> GetAllLectures()
        {
            return _lectureRepository.GetAllEntities();
        }

        public Lecture GetLectureById(int lectureId)
        {
            return _lectureRepository.GetEntityById(lectureId);
        }

        public async Task AddLecture(Lecture lecture)
        {
            _lectureRepository.AddEntity(lecture);
        }

        public async Task UpdateLecture(int id, Lecture lecture)
        {
            _lectureRepository.UpdateEntity(lecture);
        }

        public async Task DeleteLecture(Lecture lecture)
        {
            _lectureRepository.DeleteEntity(lecture.Id);
        }
    }
}