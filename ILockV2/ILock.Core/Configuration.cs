using Microsoft.Extensions.Configuration;

namespace ILock.Core
{
    /// <summary>
    /// The configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The configuration section.
        /// </summary>
        private const string configurationSection = "ILock";
        public TokenSettings TokenSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Configuration(IConfiguration configuration)
        {
            TokenSettings = new TokenSettings();
            configuration.GetRequiredSection(configurationSection).Bind(this);
        }
    }
}
