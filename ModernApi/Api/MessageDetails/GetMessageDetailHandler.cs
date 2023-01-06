using ModernApi.Data;

namespace ModernApi.Api.MessageDetails;

using MediatR;

public class GetMessageDetailHandler : IRequestHandler<GetMessageDetail, MessageDetailResponse?>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessageDetailHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDetailResponse?> Handle(GetMessageDetail request, CancellationToken cancellationToken)
    {
        var messageDetail = await _messageRepository.GetMessageDetail(request.MessageGuid);
        if(messageDetail != null)
        {
            return new MessageDetailResponse{
                MessageGuid = messageDetail.MessageGuid,
                Body = messageDetail.Body,
                Subject = messageDetail.Subject
            };
        }
        return null;
    }
}