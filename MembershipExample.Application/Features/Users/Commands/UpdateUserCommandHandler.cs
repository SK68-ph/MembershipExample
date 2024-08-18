using AutoMapper;
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

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            // check if the username is already taken
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new UsernameAlreadyTakenException("Username is already taken");
            }

            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

    }
}
