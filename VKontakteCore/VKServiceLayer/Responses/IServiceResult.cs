namespace VKServiceLayer.Responses
{
    public interface IServiceResult
    {
        bool ResponseIsSuccessful();
        string Error { get; set; }
    }
}
