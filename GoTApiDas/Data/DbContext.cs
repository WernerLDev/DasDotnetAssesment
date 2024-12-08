using GoTApiDas.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoTApiDas.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterAlias> CharacterAliases { get; set; }
    
}
