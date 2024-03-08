using Microsoft.AspNetCore.Identity;
using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(IRepository<Student> studentRepository, UserManager<ApplicationUser> userManager)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAllEntities();
        }

        public Student GetStudentById(int studentId)
        {
            return _studentRepository.GetEntityById(studentId);
        }

        public async Task AddStudent(Student student, string email)
        {
            var user = new ApplicationUser() { UserName = student.IdentityNo, Email = email };
            var result = await _userManager.CreateAsync(user, $"Aa.{student.IdentityNo}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Student");

                if (addToRoleResult.Succeeded)
                {
                    student.UserId = user.Id;
                    _studentRepository.AddEntity(student);
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

        public async Task UpdateStudent(int id, Student student)
        {
            var existingStudent = _studentRepository.GetEntityById(id);

            if (existingStudent != null)
            {
                existingStudent.Firstname = student.Firstname;
                existingStudent.Surname = student.Surname;
                existingStudent.Birth = student.Birth;
                existingStudent.Phone = student.Phone;
                existingStudent.IdentityNo = student.IdentityNo;
                existingStudent.Faculty = student.Faculty;
                existingStudent.Department = student.Department;
                existingStudent.Year = student.Year;

                _studentRepository.UpdateEntity(existingStudent);
            }
        }

        public async Task DeleteStudent(Student student)
        {
            var user = await _userManager.FindByIdAsync(student.UserId);
            if (user == null)
            {
                throw new ApplicationException($"User is not found");
            }

            await _userManager.DeleteAsync(user);
            _studentRepository.DeleteEntity(student.Id);
        }
    }
}
