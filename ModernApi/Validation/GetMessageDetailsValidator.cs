namespace ModernApi.Validation
{
    using FluentValidation;
    using Model;

    public class GetMessageDetailsValidator : AbstractValidator<GetMessageDetails>
    {
        public GetMessageDetailsValidator()
        {
            RuleFor(r => r.MessageGuid).NotEmpty();
        }
    }
}
