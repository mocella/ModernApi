namespace ModernApi.Api.MessageDetails;

using MediatR;

public class GetMessageDetailHandler : IRequestHandler<GetMessageDetails, MessageDetailsResponse?>
{
    private readonly IMediator _mediator;

    public GetMessageDetailHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<MessageDetailsResponse?> Handle(GetMessageDetails request, CancellationToken cancellationToken)
    {
        // TODO: setup an EF Query, then map to DTOs as the response
        return new MessageDetailsResponse(request.MessageGuid);
    }
}