using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using Students_Management_Api.Services.StudentServices;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = _studentService.GetAllStudents();

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model)
        {
            try
            {
                var student = _mapper.Map<Student>(model);
                await _studentService.AddStudent(student, model.Email);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentViewModel model)
        {
            var student = _mapper.Map<Student>(model);
            await _studentService.UpdateStudent(id, student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var existingStudent = _studentService.GetStudentById(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            _studentService.DeleteStudent(existingStudent);
            return NoContent();
        }
    }
}
