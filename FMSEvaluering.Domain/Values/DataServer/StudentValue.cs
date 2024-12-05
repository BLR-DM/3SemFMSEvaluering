namespace FMSEvaluering.Domain.Values.DataServer
{
    public record StudentValue
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ModelClassValue Class { get; set; }
        public string AppUserId { get; set; }
    }
}
