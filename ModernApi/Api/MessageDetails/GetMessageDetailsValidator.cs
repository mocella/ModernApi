namespace ModernApi.Api.MessageDetails;

using FluentValidation;

public class GetMessageDetailsValidator : AbstractValidator<GetMessageDetail>
{
    public GetMessageDetailsValidator()
    {
        RuleFor(r => r.MessageGuid).NotEmpty();
    }
}