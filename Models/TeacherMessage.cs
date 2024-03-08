using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Students_Management_Api.Models
{
    public class TeacherMessage : Message
    {
        [ForeignKey("Teacher")]
        public int SenderId { get; set; }
        public Teacher Sender { get; set; }
        public List<Student>? Receivers { get; set; } = new List<Student>();
    }
    public class TeacherMessageViewModel
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public Boolean? Status { get; set; }
        public List<int> ReceiverIds { get; set; } = new List<int>();
    }

    public class StudentTeacherMessage
    {
        [ForeignKey("Student")]
        public int ReceiverId { get; set; }
        public Student Receiver { get; set; }
        [ForeignKey("TeacherMessage")]
        public int ReceivedId { get; set; }
        public TeacherMessage Received { get; set; }
    }
}
