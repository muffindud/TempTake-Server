namespace TempTake_Server.Models
{
    public class ManagerWorker
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int ManagerId { get; set; }
        public int WorkerId { get; set; }
    }
}