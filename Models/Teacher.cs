using System.ComponentModel.DataAnnotations.Schema;

namespace Students_Management_Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birth { get; set; }
        public string? Phone { get; set; }
        public string IdentityNo { get; set; }
        public string? Study { get; set; }
        public List<Class>? Classes { get; set; } = new List<Class>();
        public List<Lecture>? Lectures { get; set; } = new List<Lecture>();
        public List<StudentMessages>? Received { get; set; } = new List<StudentMessages>();
        public List<TeacherMessage>? Sents { get; set; } = new List<TeacherMessage>();
    }

    public class TeacherViewModel
    {
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birth { get; set; }
        public string? Phone { get; set; }
        public string IdentityNo { get; set; }
        public string? Study { get; set; }
        public string Email { get; set; }
    }
}
