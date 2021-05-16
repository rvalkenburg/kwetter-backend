namespace Kwetter.Services.FollowService.Application.Events
{
    public class Event<T>
    {
        public T Data { get; set; }
    }
}