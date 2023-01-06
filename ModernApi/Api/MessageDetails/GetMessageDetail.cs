namespace ModernApi.Api.MessageDetails;

using MediatR;

public class GetMessageDetail : IRequest<MessageDetailResponse?>
{
    public GetMessageDetail(Guid messageGuid)
    {
        MessageGuid = messageGuid;
    }

    public Guid MessageGuid { get; }
}

public class MessageDetailResponse
{
    public Guid MessageGuid { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}