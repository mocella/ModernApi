using Microsoft.EntityFrameworkCore;

namespace ModernApi.Data;

public class MessageRepository : IMessageRepository
{
    private readonly MessageContext _messageContext;

    public MessageRepository(MessageContext messageContext)
    {
        _messageContext = messageContext;
    }

    public Task<Message?> GetMessageDetail(Guid messageId)
    {
        return _messageContext.Messages!
            .FirstOrDefaultAsync(m => m.MessageGuid == messageId);
    }
}