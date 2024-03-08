using Microsoft.AspNetCore.Identity;
using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.TeacherServices
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherService(IRepository<Teacher> teacherRepository, UserManager<ApplicationUser> userManager)
        {
            _teacherRepository = teacherRepository;
            _userManager = userManager;
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAllEntities();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return _teacherRepository.GetEntityById(teacherId);
        }

        public async Task AddTeacher(Teacher teacher, string email)
        {
            var user = new ApplicationUser() { UserName = teacher.IdentityNo, Email = email };
            var result = await _userManager.CreateAsync(user, $"Aa.{teacher.IdentityNo}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Teacher");

                if (addToRoleResult.Succeeded)
                {
                    teacher.UserId = user.Id;
                    _teacherRepository.AddEntity(teacher);
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    throw new ApplicationException($"Failed to assign the user to the role: {string.Join(", ", addToRoleResult.Errors)}");
                }
            }
            else
            {
                throw new ApplicationException($"{string.Join(", ", result.Errors)}");
            }
        }

        public async Task UpdateTeacher(int id, Teacher teacher)
        {
            var existingStudent = _teacherRepository.GetEntityById(id);

            if (existingStudent != null)
            {
                existingStudent.Firstname = teacher.Firstname;
                existingStudent.Surname = teacher.Surname;
                existingStudent.Birth = teacher.Birth;
                existingStudent.Phone = teacher.Phone;
                existingStudent.IdentityNo = teacher.IdentityNo;
                existingStudent.Study = teacher.Study;

                _teacherRepository.UpdateEntity(existingStudent);
            }
        }

        public async Task DeleteTeacher(Teacher teacher)
        {
            var user = await _userManager.FindByIdAsync(teacher.UserId);
            if (user == null)
            {
                throw new ApplicationException($"User is not found");
            }

            await _userManager.DeleteAsync(user);
            _teacherRepository.DeleteEntity(teacher.Id);
        }
    }
}
