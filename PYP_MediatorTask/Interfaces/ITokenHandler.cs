using PYP_MediatorTask.DTOs.Token;
using PYP_MediatorTask.Model;

namespace PYP_MediatorTask.Interfaces
{
    public interface ITokenHandler
    {
       TokenDto CreateAccessToken(int expiration, AppUser user, IList<string> role);
    }
}
