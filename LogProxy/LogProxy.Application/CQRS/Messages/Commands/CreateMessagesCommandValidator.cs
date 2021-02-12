using FluentValidation;

namespace LogProxy.Application.CQRS.Messages.Commands
{
    public class CreateMessagesCommandValidator : AbstractValidator<CreateMessagesCommand>
    {
        public CreateMessagesCommandValidator()
        {
            RuleFor(x => x.Messages)
                .Must(x => x.Count > 0).WithMessage("At least one message must exist");

            RuleForEach(x => x.Messages).ChildRules(msgs => {
                msgs.RuleFor(x => x.Id).NotEmpty().WithMessage("Message Id must not be empty");
                msgs.RuleFor(x => x.Title).NotEmpty().WithMessage("Message Title must not be empty");
                msgs.RuleFor(x => x.Text).NotEmpty().WithMessage("Message Text must not be empty");
            });
        }
    }
}