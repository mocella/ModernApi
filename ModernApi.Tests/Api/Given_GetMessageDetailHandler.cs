using System;
using System.Threading;
using System.Threading.Tasks;
using ModernApi.Api.MessageDetails;
using ModernApi.Data;
using Moq;
using Xunit;

namespace ModernApi.Tests.Api;

public class Given_GetMessageDetailHandler
{
    private readonly Mock<IMessageRepository> _messageRepository;
    private readonly CancellationToken _cancellationToken;
    private readonly GetMessageDetailHandler _handler;

    public Given_GetMessageDetailHandler()
    {
        _messageRepository = new Mock<IMessageRepository>();
        _cancellationToken = new CancellationToken();
        _handler = new GetMessageDetailHandler(_messageRepository.Object);
    }
    
    [Fact]
    public async Task Should_Return_Expected_MessageDetail()
    {
        // arrange 
        var messageGuid = Guid.NewGuid();
        var request = new GetMessageDetail(messageGuid);
        var mockMessage = new Message
        {
            MessageGuid = messageGuid,
            Body = Faker.Lorem.Paragraph(),
            Subject = Faker.Lorem.Sentence()
        };
        _messageRepository.Setup(r => r.GetMessageDetail(messageGuid))
            .ReturnsAsync(mockMessage);

        // act 
        var response = await _handler.Handle(request, _cancellationToken);

        // assert
        Assert.NotNull(response);
        Assert.Equal(mockMessage.MessageGuid, response!.MessageGuid);
        Assert.Equal(mockMessage.Body, response!.Body);
        Assert.Equal(mockMessage.Subject, response!.Subject);
    }

    [Fact]
    public async Task Should_Return_Null_When_Not_Found()
    {
        // arrange 
        var messageGuid = Guid.NewGuid();
        var request = new GetMessageDetail(messageGuid);
       
        _messageRepository.Setup(r => r.GetMessageDetail(messageGuid))
            .ReturnsAsync((Message?)null);

        // act 
        var response = await _handler.Handle(request, _cancellationToken);

        // assert
        Assert.Null(response);
    }
}