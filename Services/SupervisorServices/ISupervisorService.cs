using Students_Management_Api.Models;

namespace Students_Management_Api.Services.SupervisorServices
{
    public interface ISupervisorService
    {
        IEnumerable<Supervisor> GetAllSupervisors();
        Supervisor GetSupervisorById(int supervisorId);
        Task AddSupervisor(Supervisor supervisor, string email);
        Task UpdateSupervisor(int id, Supervisor supervisor);
        Task DeleteSupervisor(Supervisor supervisor);
    }
}
