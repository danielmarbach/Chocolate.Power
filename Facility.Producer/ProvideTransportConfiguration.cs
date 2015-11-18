using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Facility.Producer
{
    public class ProvideTransportConfiguration : IProvideConfiguration<TransportConfig>
    {
        public TransportConfig GetConfiguration()
        {
            return new TransportConfig { MaximumConcurrencyLevel = Constants.MaxConcurrency };
        }
    }
}