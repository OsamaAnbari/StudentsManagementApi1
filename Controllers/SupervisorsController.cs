using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using Students_Management_Api.Services.SupervisorServices;

namespace Students_Management_Api.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        private readonly IMapper _mapper;

        public SupervisorsController(ISupervisorService supervisorService, IMapper mapper)
        {
            _supervisorService = supervisorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Supervisor>> GetAllSupervisors()
        {
            var supervisors = _supervisorService.GetAllSupervisors();

            if (supervisors == null)
            {
                return NotFound();
            }

            return Ok(supervisors);
        }

        [HttpGet("{id}")]
        public ActionResult<Supervisor> GetSupervisorById(int id)
        {
            var supervisor = _supervisorService.GetSupervisorById(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupervisor([FromBody] SupervisorViewModel model)
        {
            try
            {
                var supervisor = _mapper.Map<Supervisor>(model);
                await _supervisorService.AddSupervisor(supervisor, model.Email);
                return CreatedAtAction(nameof(GetSupervisorById), new { id = supervisor.Id }, supervisor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor(int id, [FromBody] SupervisorViewModel model)
        {
            var supervisor = _mapper.Map<Supervisor>(model);
            await _supervisorService.UpdateSupervisor(id, supervisor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupervisor(int id)
        {
            var existingSupervisor = _supervisorService.GetSupervisorById(id);

            if (existingSupervisor == null)
            {
                return NotFound();
            }

            _supervisorService.DeleteSupervisor(existingSupervisor);
            return NoContent();
        }
    }
}