using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Memberships.Queries
{
    public class GetMembershipsByUserIdQueryHandler : IRequestHandler<GetMembershipsByUserIdQuery, List<MembershipDto>>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetMembershipsByUserIdQuery> _validator;

        public GetMembershipsByUserIdQueryHandler(IMembershipRepository membershipRepository, IMapper mapper, IValidator<GetMembershipsByUserIdQuery> validator)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<List<MembershipDto>> Handle(GetMembershipsByUserIdQuery request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Retrieve memberships by user ID
            var memberships = await _membershipRepository.GetByUserIdAsync(request.UserId);

            // Map the memberships to MembershipDto and return them
            return _mapper.Map<List<MembershipDto>>(memberships);
        }
    }
}
