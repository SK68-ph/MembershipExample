using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Commands
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, PlanDto>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePlanCommand> _validator;

        public CreatePlanCommandHandler(IPlanRepository planRepository, IMapper mapper, IValidator<CreatePlanCommand> validator)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PlanDto> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var plan = new Domain.Entities.Plan
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                DurationInDays = request.DurationInDays
            };

            plan = await _planRepository.CreateAsync(plan);

            return _mapper.Map<PlanDto>(plan);
        }
    }
}

