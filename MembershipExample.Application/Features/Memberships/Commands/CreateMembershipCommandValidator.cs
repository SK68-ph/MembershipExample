using FluentValidation;
using MembershipExample.Application.Features.Memberships.Commands;

public class CreateMembershipCommandValidator : AbstractValidator<CreateMembershipCommand>
{
    public CreateMembershipCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.PlanId).GreaterThan(0);
    }
}
