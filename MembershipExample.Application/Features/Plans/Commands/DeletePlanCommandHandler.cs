using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Plans.Commands
{
    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, bool>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public DeletePlanCommandHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.Id);
            if (plan == null)
            {
                throw new PlanNotFoundException($"Plan with id {request.Id} not found.");
            }

            await _planRepository.DeleteAsync(plan.Id);

            return true;
        }
    }
}
