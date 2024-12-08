using GoTApiDas.Models.Entities;
using GoTApiDas.Models.IceOfFireApi;
using GoTApiDas.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoTApiDas.Data.Repositories;

public class CharacterRepository(ApiDbContext dbContext) : ICharacterRepository
{
  public async Task<Character> AddCharacter(Character character)
  {
    dbContext.Add(character);
    await dbContext.SaveChangesAsync();
    return character;
  }

  public List<Character> GetCharacters()
  {
    return dbContext.Characters
      .Include(character => character.CharacterAliases)
      .ToList();
  }

  public async Task ImportCharacter(ApiCharacter character)
  {
    var createdCharacter = await AddCharacter(new Character()
    {
      Name = character.Name,
      Gender = character.Gender,
      Born = character.Born,
      Culture = character.Culture,
      Died = character.Died
    });

    foreach (var alias in character.Aliases)
    {
      await AddAlias(createdCharacter, alias);
    }
  }

  public async Task AddAlias(Character character, string alias)
  {
    dbContext.Add(new CharacterAlias()
    {
      Alias = alias,
      CharacterId = character.Id,
      Character = character
    });

    await dbContext.SaveChangesAsync();
  }

  public async Task<Character?> FindByName(string name)
  {
    return await dbContext.Characters
      .Where(c => c.Name.Equals(name))
      .FirstOrDefaultAsync();
  }

  public List<Character> FindByAlias(string alias, int page)
  {
    const int pageSize = 10;

    return dbContext.Characters
      .Include(c => c.CharacterAliases)
      .Where(c => c.CharacterAliases.Any(a => a.Alias.Contains(alias)))
      .Skip(page * pageSize)
      .Take(pageSize)
      .ToList();
  }
}
