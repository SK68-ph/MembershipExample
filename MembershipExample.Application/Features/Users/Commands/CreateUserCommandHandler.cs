using AutoMapper;
using FluentValidation;
using MediatR;
using MembershipExample.Application.DTOs;
using MembershipExample.Application.Exceptions;
using MembershipExample.Domain.Entities;
using MembershipExample.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembershipExample.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Check if the username is already taken
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException("Username is already taken");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var user = new User
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                PasswordSalt = salt,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt),
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }
    }

}
