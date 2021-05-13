namespace Kwetter.Services.KweetService.Application.Events
{
    public abstract class BaseEvent
    {
        public string Topic { get; set; }
    }
}