namespace TempTake_Server.Models
{
    public class UserManager
    {
        // Primary Key
        public int Id { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;

        // Foreign Keys
        public int UserId { get; set; }
        public User User { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

    }
}