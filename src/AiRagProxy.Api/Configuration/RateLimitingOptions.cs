namespace AiRagProxy.Api.Configuration
{
    /// <summary>
    /// Represents the configuration options for rate limiting in the application.
    /// </summary>
    public class RateLimitingOptions
    {
        /// <summary>
        /// Gets or sets the maximum number of permits allowed within the rate limiting window.
        /// Default value is 100.
        /// </summary>
        public int PermitLimit { get; set; } = 100;

        /// <summary>
        /// Gets or sets the duration of the rate limiting window in minutes.
        /// Default value is 1 minute.
        /// </summary>
        public int WindowMinutes { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum number of requests that can be queued.
        /// Default value is 0.
        /// </summary>
        public int QueueLimit { get; set; } = 0;
    }
}