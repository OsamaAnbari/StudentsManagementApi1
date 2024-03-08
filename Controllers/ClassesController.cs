using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api;
using Students_Management_Api.Models;
using Students_Management_Api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Students_Management_Api.Controllers
{
    //[Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ClassesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClass()
        {
            if (_context.Class == null)
            {
                return NotFound();
            }
            return await _context.Class.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            if (_context.Class == null)
            {
                return NotFound();
            }
            var @class = await _context.Class.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, ClassViewModel model)
        {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Attach(@class);
            _context.Entry(@class).CurrentValues.SetValues(model);

            var toRemove = _context.ClassStudent.Where(e => e.ClassesId == id).ToList();
            _context.ClassStudent.RemoveRange(toRemove);

            foreach (var studentId in model.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.ClassStudent.Add(
                    new ClassStudent()
                    {
                        Class = @class,
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
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(ClassViewModel model)
        {
            if (_context.Class == null)
            {
                return Problem("Entity set 'LibraryContext.Class'  is null.");
            }

            if (!TeacherExists(model.TeacherID))
            {
                return BadRequest(new { error = "Teacher is not found" });
            }

            var @class = new Class()
            {
                Time = model.Time,
                TeacherID = model.TeacherID
            };

            foreach (var studentId in model.StudentIds)
            {
                if (StudentExists(studentId))
                {
                    _context.ClassStudent.Add(
                    new ClassStudent()
                    {
                        Class = @class,
                        StudentsId = studentId
                    });
                }
            }

            _context.Class.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (_context.Class == null)
            {
                return NotFound();
            }
            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return (_context.Class?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool TeacherExists(int? id)
        {
            return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
