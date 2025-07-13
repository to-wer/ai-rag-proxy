namespace AiRagProxy.Api.Configurations
{
    public class RateLimitingOptions
    {
        public int PermitLimit { get; set; } = 100;
        public int WindowMinutes { get; set; } = 1;
        public int QueueLimit { get; set; } = 0;
    }
}

