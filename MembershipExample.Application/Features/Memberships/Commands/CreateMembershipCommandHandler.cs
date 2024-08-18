using AutoMapper;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Features.Memberships.Commands;
using MembershipExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Membership.Commands
{
    public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, MembershipDto>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public CreateMembershipCommandHandler(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<MembershipDto> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
        {
            var membership = await _membershipRepository.CreateAsync(new Domain.Entities.Membership
            {
                UserId = request.UserId,
                PlanId = request.PlanId
            });

            return _mapper.Map<MembershipDto>(membership);
        }

    }
}
