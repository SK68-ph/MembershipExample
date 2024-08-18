using FluentValidation;
using MembershipExample.Application.Features.Memberships.Queries;

public class GetMembershipByIdQueryValidator : AbstractValidator<GetMembershipByIdQuery>
{
    public GetMembershipByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

