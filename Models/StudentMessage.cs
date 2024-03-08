using System.ComponentModel.DataAnnotations.Schema;

namespace Students_Management_Api.Models
{
    public class StudentMessages : Message
    {
        [ForeignKey("Student")]
        public int SenderId { get; set; }
        public Student? Sender { get; set; }
        [ForeignKey("Teacher")]
        public int ReceiverId { get; set; }
        public Teacher? Receiver { get; set; }
    }

    public class StudentMessagesViewModel
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public Boolean? Status { get; set; }
        public int ReceiverId { get; set; }
    }
}
