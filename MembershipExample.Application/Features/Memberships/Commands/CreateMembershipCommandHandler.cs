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

namespace MembershipExample.Application.Features.Memberships.Commands
{
    public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, MembershipDto>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMembershipCommand> _validator;

        public CreateMembershipCommandHandler(IMembershipRepository membershipRepository, IMapper mapper, IValidator<CreateMembershipCommand> validator)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<MembershipDto> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var membership = await _membershipRepository.CreateAsync(new Domain.Entities.Membership
            {
                UserId = request.UserId,
                PlanId = request.PlanId
            });

            return _mapper.Map<MembershipDto>(membership);
        }
    }
}
