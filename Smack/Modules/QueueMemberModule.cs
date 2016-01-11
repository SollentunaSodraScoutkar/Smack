using Nancy;

namespace Smack.Modules
{
    public class QueueMemberModule : NancyModule
    {
        public QueueMemberModule() : base("smack/queuemembers")
        {
            Post["/"] = x => AddQueueMember();
        }

        private HttpStatusCode AddQueueMember()
        {
            throw new System.NotImplementedException();
        }
    }
}


