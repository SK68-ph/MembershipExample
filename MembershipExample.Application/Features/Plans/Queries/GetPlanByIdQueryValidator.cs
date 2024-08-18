using FluentValidation;
using MembershipExample.Application.Features.Plans.Queries;

public class GetPlanByIdQueryValidator : AbstractValidator<GetPlanByIdQuery>
{
    public GetPlanByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
