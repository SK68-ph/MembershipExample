using FluentValidation;
using MembershipExample.Application.Features.Users.Queries;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

