using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using System.Security.Claims;

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMessagesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public StudentMessagesController(LibraryContext context)
        {
            _context = context;
        }

        // POST: api/StudentMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentMessages>> PostStudentMessage(StudentMessagesViewModel model)
        {
            if (_context.StudentMessages == null)
            {
                return Problem("Entity set 'LibraryContext.StudentMessages'  is null.");
            }

            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int studentId = _context.Student.FirstOrDefault(e => e.UserId == userId).Id;
            if (!StudentExists(studentId))
            {
                return BadRequest(new { error = "Teacher is not found" });
            }

            var teacherMessage = new StudentMessages()
            {
                SenderId = studentId,
                ReceiverId = model.ReceiverId,
                Subject = model.Subject,
                Status = model.Status,
                Date = model.Date,
                Body = model.Body
            };

            _context.StudentMessages.Add(teacherMessage);
            await _context.SaveChangesAsync();

            return Ok(new { id = teacherMessage.Id, teacherMessage });
        }

        // DELETE: api/StudentMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentMessage(int id)
        {
            if (_context.StudentMessages == null)
            {
                return NotFound();
            }
            var teacherMessage = await _context.StudentMessages.FindAsync(id);
            if (teacherMessage == null)
            {
                return NotFound();
            }

            _context.StudentMessages.Remove(teacherMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentMessageExists(int id)
        {
            return (_context.StudentMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool TeacherExists(int id)
        {
            return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}