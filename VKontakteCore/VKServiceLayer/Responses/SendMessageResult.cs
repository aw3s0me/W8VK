namespace VKServiceLayer.Responses
{
    public class SendMessageResult : IServiceResult
    {
        public int Response { get; set; }
        public bool ResponseIsSuccessful()
        {
            return Response != 0;
        }

        public string Error { get; set; }
    }
}
