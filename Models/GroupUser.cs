namespace TempTake_Server.Models
{
    public class GroupUser
    {
        // Primary Key
        public int Id { get; set; }
        
        public bool IsAdmin { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
        
        // Foreign Keys
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}