using FluentValidation;
using MembershipExample.Application.Features.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Username).NotEmpty().Length(3, 50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
