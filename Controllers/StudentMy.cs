using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System.Security.Cryptography;
using Students_Management_Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging.Console;
using System.Security.Claims;

namespace Students_Management_Api.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("api/Students/My")]
    [ApiController]
    public class StudentMy : ControllerBase
    {
        private readonly LibraryContext _context;
        IDistributedCache _cache;
        ILogger _logger;

        public StudentMy(LibraryContext context, IDistributedCache cache, ILogger<StudentMy> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet("infos")]
        public async Task<ActionResult<Student>> GetMyInfos()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var student = _context.Student.FirstOrDefault(e => e.UserId == userId);


            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<List<Lecture>>> GetMyLectures()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var student = _context.Student.Include(e => e.Lectures).FirstOrDefault(e => e.UserId == userId);


            if (student == null)
            {
                return NotFound();
            }

            return student.Lectures;
        }

        [HttpGet("class")]
        public async Task<ActionResult<List<Class>>> GetMyClasses()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var student = _context.Student.Include(e => e.Classes).FirstOrDefault(e => e.UserId == userId);


            if (student == null)
            {
                return NotFound();
            }

            return student.Classes;
        }

        [HttpGet("sents")]
        public async Task<ActionResult<List<StudentMessages>>> GetMySents()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var student = _context.Student.Include(e => e.Sents).FirstOrDefault(e => e.UserId == userId);


            if (student == null)
            {
                return NotFound();
            }

            return student.Sents;
        }

        [HttpGet("received")]
        public async Task<ActionResult<List<TeacherMessage>>> GetMyReceived()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var student = _context.Student.Include(e => e.Received).FirstOrDefault(e => e.UserId == userId);


            if (student == null)
            {
                return NotFound();
            }

            return student.Received;
        }
    }
}
