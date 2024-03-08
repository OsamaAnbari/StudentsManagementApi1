namespace Students_Management_Api.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public Boolean? Status { get; set; }
    }
}
