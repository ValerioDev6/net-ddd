using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArquitectura.Application.Abstractions.Email;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Users.Events;
using MediatR;

namespace CleanArquitectura.Application.Users.RegisterUser
{
    public sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;

        private readonly IEmailService _emailService;

        public UserCreatedDomainEventHandler(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(
                notification.UserId,
                cancellationToken
            );

            if (user is null)
            {
                return;
            }

            await _emailService.SendAsync(
                user.Email!,
                "Se ha crado su cuenta en nuyesta app",
                "Tiene suna neuva cuetna dentro de la plataforma"
            );

        }
    }
}
