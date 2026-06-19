namespace ImpedexTracker.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;
        public string Status { get; set; } = "Received";
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}