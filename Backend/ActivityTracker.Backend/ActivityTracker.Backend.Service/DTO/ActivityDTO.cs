namespace ActivityTracker.Backend.Service.DTO
{
    public class ActivityDTO
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}
