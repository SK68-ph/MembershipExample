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

namespace MembershipExample.Application.Features.Memberships.Commands
{
    public class UpdateMembershipCommandHandler : IRequestHandler<UpdateMembershipCommand, MembershipDto>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateMembershipCommand> _validator;

        public UpdateMembershipCommandHandler(IMembershipRepository membershipRepository, IMapper mapper, IValidator<UpdateMembershipCommand> validator)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<MembershipDto> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var membership = await _membershipRepository.GetByIdAsync(request.Id);
            if (membership == null)
            {
                throw new MembershipNotFoundException($"Membership with id {request.Id} not found.");
            }

            membership.PlanId = request.PlanId;
            await _membershipRepository.UpdateAsync(membership);

            return _mapper.Map<MembershipDto>(membership);
        }
    }

}