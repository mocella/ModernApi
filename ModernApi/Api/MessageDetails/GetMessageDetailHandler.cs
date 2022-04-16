namespace ModernApi.Handlers;

using MediatR;
using Model;

public class GetMessageDetailHandler : IRequestHandler<GetMessageDetails, MessageDetailsResponse?>
{
    public async Task<MessageDetailsResponse?> Handle(GetMessageDetails request, CancellationToken cancellationToken)
    {
        return new MessageDetailsResponse(request.MessageGuid);
    }
}