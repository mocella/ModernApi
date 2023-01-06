namespace ModernApi.Data;

public interface IMessageRepository
{
    Task<Message?> GetMessageDetail(Guid messageId);
}