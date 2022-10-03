using MediatR;

namespace PYP_MediatorTask.Features.Commands
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; } = null!;
        public string Surnmae { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
