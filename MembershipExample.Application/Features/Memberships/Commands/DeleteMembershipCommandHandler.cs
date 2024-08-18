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
    public class DeleteMembershipCommandHandler : IRequestHandler<DeleteMembershipCommand, bool>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteMembershipCommand> _validator;

        public DeleteMembershipCommandHandler(IMembershipRepository membershipRepository, IMapper mapper, IValidator<DeleteMembershipCommand> validator)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteMembershipCommand request, CancellationToken cancellationToken)
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

            await _membershipRepository.DeleteAsync(request.Id);

            return true;
        }
    }
}
