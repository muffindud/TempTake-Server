namespace TempTake_Server.Models
{
    public class ManagerWorker
    {
        // Primary Key
        public int Id { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;

        // Foreign Keys
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}