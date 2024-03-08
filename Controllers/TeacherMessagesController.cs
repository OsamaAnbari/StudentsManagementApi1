using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherMessagesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public TeacherMessagesController(LibraryContext context)
        {
            _context = context;
        }

        // POST: api/TeacherMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherMessage>> PostTeacherMessage(TeacherMessageViewModel model)
        {
            if (_context.TeacherMessage == null)
            {
                return Problem("Entity set 'LibraryContext.TeacherMessage'  is null.");
            }

            //string userId = HttpContext.Items["userId"]?.ToString();
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int teacherId = _context.Teacher.FirstOrDefault(e => e.UserId == userId).Id;
            if (!TeacherExists(teacherId))
            {
                return BadRequest(new { error = "Teacher is not found" });
            }

            var teacherMessage = new TeacherMessage()
            {
                SenderId = teacherId,
                Subject = model.Subject,
                Status = model.Status,
                Date = model.Date,
                Body = model.Body
            };

            foreach (var studentId in model.ReceiverIds)
            {
                if (StudentExists(studentId))
                {
                    _context.StudentTeacherMessage.Add(
                    new StudentTeacherMessage()
                    {
                        Received = teacherMessage,
                        ReceiverId = studentId
                    });
                }
            }

            _context.TeacherMessage.Add(teacherMessage);
            await _context.SaveChangesAsync();

            return Ok(new { id = teacherMessage.Id, teacherMessage });
        }

        // DELETE: api/TeacherMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherMessage(int id)
        {
            if (_context.TeacherMessage == null)
            {
                return NotFound();
            }
            var teacherMessage = await _context.TeacherMessage.FindAsync(id);
            if (teacherMessage == null)
            {
                return NotFound();
            }

            _context.TeacherMessage.Remove(teacherMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherMessageExists(int id)
        {
            return (_context.TeacherMessage?.Any(e => e.Id == id)).GetValueOrDefault();
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