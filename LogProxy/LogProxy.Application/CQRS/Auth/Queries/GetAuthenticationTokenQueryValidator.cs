using FluentValidation;

namespace LogProxy.Application.CQRS.Auth.Queries
{
    public class GetAuthenticationTokenQueryValidator : AbstractValidator<GetAuthenticationTokenQuery>
    {
        public GetAuthenticationTokenQueryValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(5);

            RuleFor(v => v.Password)
                .NotEmpty()
                .MaximumLength(4);
        }
    }
}
