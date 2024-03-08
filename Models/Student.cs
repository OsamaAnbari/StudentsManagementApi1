using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_Management_Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birth { get; set; }
        public string? Phone { get; set; }
        public string IdentityNo { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Year { get; set; }
        public List<Class>? Classes { get; set; } = new List<Class>();
        public List<Lecture>? Lectures { get; } = new List<Lecture>();
        public List<StudentMessages>? Sents { get; set; } = new List<StudentMessages>();
        public List<TeacherMessage>? Received { get; set; } = new List<TeacherMessage>();
    }

    public class StudentViewModel
    {
        [Required(ErrorMessage = "Firstame is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string? Surname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birth { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits are allowed")]
        public string? Phone { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits are allowed")]
        public string IdentityNo { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters are allowed")]
        public string? Faculty { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters are allowed")]
        public string? Department { get; set; }

        public string? Year { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
