using MediatR;

namespace PYP_MediatorTask.Features.Commands
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
