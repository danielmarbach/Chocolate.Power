using System.Threading;
using Messages;
using NServiceBus;

namespace Facility.Producer
{
    public class Handler : IHandleMessages<ProduceChocolateBar>
    {
        public void Handle(ProduceChocolateBar message)
        {
            Thread.Sleep(100);
            Syncher.SyncEvent.Signal();
        }
    }
}