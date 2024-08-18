using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Queries
{
    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, PlanDto>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetPlanByIdQuery> _validator;

        public GetPlanByIdQueryHandler(IPlanRepository planRepository, IMapper mapper, IValidator<GetPlanByIdQuery> validator)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PlanDto> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<PlanDto>(plan);
        }
    }
}
