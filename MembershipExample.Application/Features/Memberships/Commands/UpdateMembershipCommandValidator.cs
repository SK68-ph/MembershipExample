using FluentValidation;
using MembershipExample.Application.Features.Memberships.Commands;

public class UpdateMembershipCommandValidator : AbstractValidator<UpdateMembershipCommand>
{
    public UpdateMembershipCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.PlanId).GreaterThan(0);
    }
}
