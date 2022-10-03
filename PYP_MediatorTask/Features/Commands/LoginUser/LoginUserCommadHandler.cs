using MediatR;
using Microsoft.AspNetCore.Identity;
using PYP_MediatorTask.DTOs.Token;
using PYP_MediatorTask.Interfaces;
using PYP_MediatorTask.Model;

namespace PYP_MediatorTask.Features.Commands
{
    public class LoginUserCommadHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommadHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }


        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                TokenDto token = _tokenHandler.CreateAccessToken(3, user, roles);

             
                return new()
                {
                    Token = token,
                };
            }
            return new() { Token = null };
        }
    }
}
