using System.ComponentModel.DataAnnotations;

namespace TempTake_Server.Dtos.Entry
{
    public class ModuleEntryDto
    {
        public int? Id { get; set; } = null;
        public string? ModuleMac { get; set; } = null;
        private DateTime _from = DateTime.SpecifyKind(DateTime.Now.AddHours(-24), DateTimeKind.Utc);
        private DateTime _to = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        
        public DateTime From
        {
            get => _from;
            set => _to = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public DateTime To
        {
            get => _to;
            set => _to = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}