using Microsoft.AspNetCore.Identity;
using Students_Management_Api.Models;
using Students_Management_Api.Repositories;

namespace Students_Management_Api.Services.SupervisorServices
{
    public class SupervisorService : ISupervisorService
    {
        private readonly IRepository<Supervisor> _supervisorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupervisorService(IRepository<Supervisor> supervisorRepository, UserManager<ApplicationUser> userManager)
        {
            _supervisorRepository = supervisorRepository;
            _userManager = userManager;
        }

        public IEnumerable<Supervisor> GetAllSupervisors()
        {
            return _supervisorRepository.GetAllEntities();
        }

        public Supervisor GetSupervisorById(int supervisorId)
        {
            return _supervisorRepository.GetEntityById(supervisorId);
        }

        public async Task AddSupervisor(Supervisor supervisor, string email)
        {
            var user = new ApplicationUser() { UserName = supervisor.IdentityNo, Email = email };
            var result = await _userManager.CreateAsync(user, $"Aa.{supervisor.IdentityNo}");

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "Supervisor");

                if (addToRoleResult.Succeeded)
                {
                    supervisor.UserId = user.Id;
                    _supervisorRepository.AddEntity(supervisor);
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

        public async Task UpdateSupervisor(int id, Supervisor supervisor)
        {
            var existingStudent = _supervisorRepository.GetEntityById(id);

            if (existingStudent != null)
            {
                existingStudent.Firstname = supervisor.Firstname;
                existingStudent.Surname = supervisor.Surname;
                existingStudent.Birth = supervisor.Birth;
                existingStudent.Phone = supervisor.Phone;
                existingStudent.IdentityNo = supervisor.IdentityNo;

                _supervisorRepository.UpdateEntity(existingStudent);
            }
        }

        public async Task DeleteSupervisor(Supervisor supervisor)
        {
            var user = await _userManager.FindByIdAsync(supervisor.UserId);
            if (user == null)
            {
                throw new ApplicationException($"User is not found");
            }

            await _userManager.DeleteAsync(user);
            _supervisorRepository.DeleteEntity(supervisor.Id);
        }
    }
}
