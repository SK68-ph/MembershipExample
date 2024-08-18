using AutoMapper;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Queries
{
    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, List<PlanDto>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public GetAllPlansQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<List<PlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all plans
            var plans = await _planRepository.GetAllAsync();

            // Map the plans to PlanDto and return them
            return _mapper.Map<List<PlanDto>>(plans);
        }
    }
}



