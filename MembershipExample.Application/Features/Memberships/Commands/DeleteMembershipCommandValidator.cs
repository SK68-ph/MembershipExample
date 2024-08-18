using FluentValidation;
using MembershipExample.Application.Features.Memberships.Commands;

public class DeleteMembershipCommandValidator : AbstractValidator<DeleteMembershipCommand>
{
    public DeleteMembershipCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
