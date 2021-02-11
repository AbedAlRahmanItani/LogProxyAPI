using FluentValidation;

namespace LogProxy.Application.CQRS.Messages.Commands
{
    public class CreateMessagesCommandValidator : AbstractValidator<CreateMessagesCommand>
    {
        public CreateMessagesCommandValidator()
        {
            
        }
    }
}
