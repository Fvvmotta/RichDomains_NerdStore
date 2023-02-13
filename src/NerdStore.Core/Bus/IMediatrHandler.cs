using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}

