using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetUserByIdQuery> _validator;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper, IValidator<GetUserByIdQuery> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}

