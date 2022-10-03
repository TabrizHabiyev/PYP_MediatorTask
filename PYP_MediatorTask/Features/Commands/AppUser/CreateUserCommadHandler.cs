using MediatR;
using Microsoft.AspNetCore.Identity;
using PYP_MediatorTask.Model;

namespace PYP_MediatorTask.Features.Commands
{
    public class CreateUserCommadHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommadHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Name = request.Name,
                Surnmae = request.Surnmae,
                UserName = request.Name+request.Surnmae,
                Email = request.Email
            }, request.Password);

            if (!result.Succeeded)
            {
                return new()
                {
                    Message = "Bad Request"
                };
            }

            return new() { Message = "User Created Succestful" };
        }
    }
}
