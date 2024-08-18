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

namespace MembershipExample.Application.Features.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Validate the request
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Retrieve the user by ID
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            //Check if  authorized to update this user
            if (request.Id != user.Id)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this user");
            }

            // Check if the username is already taken
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new UsernameAlreadyTakenException($"Username {request.Username} is already taken.");
            }

            // Update only if the new value is different
            if (user.Username != request.Username)
            {
                user.Username = request.Username;
            }

            if (user.Email != request.Email)
            {
                user.Email = request.Email;
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            user.UpdatedAt = DateTime.UtcNow;

            // Update the user in the repository
            await _userRepository.UpdateAsync(user);

            // Map the updated user to a UserDto and return it
            return _mapper.Map<UserDto>(user);
        }

    }
}
