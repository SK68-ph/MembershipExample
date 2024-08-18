using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Memberships.Queries
{
    public class GetMembershipByIdQueryHandler : IRequestHandler<GetMembershipByIdQuery, MembershipDto>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetMembershipByIdQuery> _validator;

        public GetMembershipByIdQueryHandler(IMembershipRepository membershipRepository, IMapper mapper, IValidator<GetMembershipByIdQuery> validator)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<MembershipDto> Handle(GetMembershipByIdQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<MembershipDto>(membership);
        }
    }
}

