using FluentValidation;
using MembershipExample.Application.Features.Plans.Commands;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.DurationInDays).GreaterThan(0);
    }
}

