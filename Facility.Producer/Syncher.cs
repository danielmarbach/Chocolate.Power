using System.Threading;

namespace Facility.Producer
{
    public static class Syncher
    {
        public static CountdownEvent SyncEvent = new CountdownEvent(Constants.NumberOfMessages);
    }
}