using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Students_Management_Api;
using Students_Management_Api.Models;
using Students_Management_Api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Students_Management_Api.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LecturesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectureViewModel>>> GetLecture()
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }

            var lectures = await _context.Lecture.Include(e => e.Students).ToListAsync();
            List<LectureViewModel> model = new List<LectureViewModel>();

            foreach( var lecture in lectures )
            {
                /*List<int> studentIds = new List<int>();
                foreach( var student in lecture.Students)
                {
                    studentIds.Add(student.Id);
                }*/
                model.Add(new LectureViewModel()
                {
                    Id = lecture.Id,
                    Title = lecture.Title,
                    Date = lecture.Date,
                    TeacherID = lecture.TeacherID,
                    StudentIds = lecture.Students.Select(e => e.Id).ToList()
                });
            }
            return Ok(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        // GET: api/Lectures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lecture>> GetLecture(int id)
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }
            var lecture = await _context.Lecture.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            return lecture;
        }

        // PUT: api/Lectures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecture(int id, LectureViewModel model)
        {
            var lecture = await _context.Lecture.FindAsync(id);
            _context.Lecture.Attach(lecture);
            _context.Entry(lecture).CurrentValues.SetValues(model);

            var toRemove = _context.LectureStudent.Where(e => e.LecturesId == id).ToList();
            _context.LectureStudent.RemoveRange(toRemove);

            if (!TeacherExists(model.TeacherID))
            {
                return BadRequest(new { error = "Teacher is not found" });
            }

            foreach (var studentId in model.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.LectureStudent.Add(
                    new LectureStudent()
                    {
                        Lecture = lecture,
                        StudentsId = studentId
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lectures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lecture>> PostLecture(LectureViewModel model)
        {
            if (_context.Lecture == null)
            {
                return Problem("Entity set 'LibraryContext.Lecture'  is null.");
            }

            var lecture = new Lecture()
            {
                Title = model.Title,
                Date = model.Date,
                TeacherID = model.TeacherID
            };

            foreach (var studentId in model.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.LectureStudent.Add(
                    new LectureStudent()
                    {
                        Lecture = lecture,
                        StudentsId = studentId
                    });
                }
            }

            _context.Lecture.Add(lecture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecture", new { id = lecture.Id }, lecture);
        }

        // DELETE: api/Lectures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            if (_context.Lecture == null)
            {
                return NotFound();
            }
            var lecture = await _context.Lecture.FindAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            _context.Lecture.Remove(lecture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LectureExists(int id)
        {
            return (_context.Lecture?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool LectureExistsInStudent(int lectureId, int StudentId)
        {
            return (_context.Student?.Any(e => e.Id == StudentId)).GetValueOrDefault();
        }
        private bool TeacherExists(int? id)
        {
            return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
