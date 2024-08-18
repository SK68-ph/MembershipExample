using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Commands
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, PlanDto>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePlanCommand> _validator;

        public UpdatePlanCommandHandler(IPlanRepository planRepository, IMapper mapper, IValidator<UpdatePlanCommand> validator)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PlanDto> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var plan = await _planRepository.GetByIdAsync(request.Id);
            if (plan == null)
            {
                throw new PlanNotFoundException($"Plan with id {request.Id} not found.");
            }

            plan.Name = request.Name;
            plan.Description = request.Description;
            plan.Price = request.Price;
            plan.DurationInDays = request.DurationInDays;
            await _planRepository.UpdateAsync(plan);

            return _mapper.Map<PlanDto>(plan);
        }
    }
}


