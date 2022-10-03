using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PYP_MediatorTask.Model;

namespace PYP_MediatorTask.Context
{
    public class PYP_MediatorDbContext : IdentityDbContext<AppUser>
    {
        public PYP_MediatorDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
