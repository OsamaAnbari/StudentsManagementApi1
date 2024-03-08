using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class Class
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
    }

    public class ClassViewModel
    {
        public DateTime Time { get; set; }
        public int? TeacherID { get; set; }
        public List<int> StudentIds { get; set; } = new List<int>();
    }

    public class ClassStudent
    {
        [ForeignKey("Student")]
        public int StudentsId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Class")]
        public int ClassesId { get; set; }
        public Class Class { get; set; }
    }
}