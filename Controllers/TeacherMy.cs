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
    [Authorize(Roles = "Teacher")]
    [Route("api/Teachers/My")]
    [ApiController]
    public class TeacherMy : ControllerBase
    {
        private readonly LibraryContext _context;
        IDistributedCache _cache;
        ILogger _logger;

        public TeacherMy(LibraryContext context, IDistributedCache cache, ILogger<TeacherMy> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        [HttpGet("infos")]
        public async Task<ActionResult<Teacher>> GetMyInfos()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var teacher = _context.Teacher.FirstOrDefault(e => e.UserId == userId);


            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet("lectures")]
        public async Task<ActionResult<List<Lecture>>> GetMyLectures()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var teacher = _context.Teacher.Include(e => e.Lectures).FirstOrDefault(e => e.UserId == userId);


            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Lectures;
        }

        [HttpGet("class")]
        public async Task<ActionResult<List<Class>>> GetMyClasses()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var teacher = _context.Teacher.Include(e => e.Classes).FirstOrDefault(e => e.UserId == userId);


            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Classes;
        }

        [HttpGet("sents")]
        public async Task<ActionResult<List<TeacherMessage>>> GetMySents()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var teacher = _context.Teacher.Include(e => e.Sents).FirstOrDefault(e => e.UserId == userId);


            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Sents;
        }

        [HttpGet("received")]
        public async Task<ActionResult<List<StudentMessages>>> GetMyReceived()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var teacher = _context.Teacher.Include(e => e.Received).FirstOrDefault(e => e.UserId == userId);


            if (teacher == null)
            {
                return NotFound();
            }

            return teacher.Received;
        }
    }
}
