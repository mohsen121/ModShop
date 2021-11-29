using MediatR;
using Microsoft.AspNetCore.Identity;
using ModShop.Application.Common.Exceptions;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Users.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<User>
    {
        public User Entity { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, User>
    {
        private UserManager<User> _userManager;

        public CreateAdminCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(request.Entity, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(request.Entity, "Admin");
                return request.Entity;
            }

            throw new ValidationException(new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure(string.Empty, string.Join(',', result.Errors.Select(x => x.Description))) });
        }
    }
}
