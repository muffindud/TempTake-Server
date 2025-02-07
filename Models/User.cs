namespace TempTake_Server.Models
{
    public class User
    {
        // Primary Key
        public int Id { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}