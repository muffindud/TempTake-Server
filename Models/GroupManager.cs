namespace TempTake_Server.Models
{
    public class GroupManager
    {
        // Primary Key
        public int Id { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;

        // Foreign Keys
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

    }
}