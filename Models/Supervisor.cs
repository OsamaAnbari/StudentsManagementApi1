using System.ComponentModel.DataAnnotations.Schema;

namespace Students_Management_Api.Models
{
    public class Supervisor
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
    }

    public class SupervisorViewModel
    {
        public string Firstname { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birth { get; set; }
        public string? Phone { get; set; }
        public string IdentityNo { get; set; }
        public string Email { get; set; }
    }
}
