namespace ModernApi.Api.MessageDetails;

using FluentValidation;

public class GetMessageDetailsValidator : AbstractValidator<GetMessageDetails>
{
    public GetMessageDetailsValidator()
    {
        RuleFor(r => r.MessageGuid).NotEmpty();
    }
}