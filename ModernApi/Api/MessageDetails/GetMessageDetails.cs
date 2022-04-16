namespace ModernApi.Model;

using MediatR;

public class GetMessageDetails : IRequest<MessageDetailsResponse?>
{
    public GetMessageDetails(Guid messageGuid)
    {
        MessageGuid = messageGuid;
    }

    public Guid MessageGuid { get; set; }
}

public class MessageDetailsResponse
{
    public MessageDetailsResponse(Guid messageGuid)
    {
        MessageGuid = messageGuid;
    }

    public Guid MessageGuid { get; set; }
}