using FluentValidation;
using MembershipExample.Application.Features.Memberships.Queries;

public class GetMembershipsByUserIdQueryValidator : AbstractValidator<GetMembershipsByUserIdQuery>
{
    public GetMembershipsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
    }
}
