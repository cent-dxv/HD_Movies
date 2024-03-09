

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;




namespace HD_Movies.Models;

public class Identity_Context : IdentityDbContext<Identity_User>
{
    public Identity_Context(DbContextOptions<Identity_Context> options) : base(options) { }

}
