namespace ModernApi.Api.MessageDetails;

using MediatR;

public class GetMessageDetailHandler : IRequestHandler<GetMessageDetails, MessageDetailsResponse?>
{
    public async Task<MessageDetailsResponse?> Handle(GetMessageDetails request, CancellationToken cancellationToken)
    {
        return new MessageDetailsResponse(request.MessageGuid);
    }
}