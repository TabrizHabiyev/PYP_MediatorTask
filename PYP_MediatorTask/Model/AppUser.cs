using Microsoft.AspNetCore.Identity;

namespace PYP_MediatorTask.Model
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surnmae { get; set; }
    }
}
