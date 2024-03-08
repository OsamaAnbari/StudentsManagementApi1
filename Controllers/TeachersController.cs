using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using Students_Management_Api.Services.TeacherServices;

namespace Students_Management_Api.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeachersController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> GetAllTeachers()
        {
            var teachers = _teacherService.GetAllTeachers();

            if (teachers == null)
            {
                return NotFound();
            }

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public ActionResult<Teacher> GetTeacherById(int id)
        {
            var teacher = _teacherService.GetTeacherById(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherViewModel model)
        {
            try
            {
                var teacher = _mapper.Map<Teacher>(model);
                await _teacherService.AddTeacher(teacher, model.Email);
                return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherViewModel model)
        {
            var teacher = _mapper.Map<Teacher>(model);
            await _teacherService.UpdateTeacher(id, teacher);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var existingTeacher = _teacherService.GetTeacherById(id);

            if (existingTeacher == null)
            {
                return NotFound();
            }

            _teacherService.DeleteTeacher(existingTeacher);
            return NoContent();
        }
    }
}