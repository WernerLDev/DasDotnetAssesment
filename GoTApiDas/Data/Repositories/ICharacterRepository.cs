using GoTApiDas.Models.Entities;
using GoTApiDas.Models.IceOfFireApi;

namespace GoTApiDas.Repositories;

public interface ICharacterRepository
{
  public Task<Character> AddCharacter(Character character);
  public List<Character> GetCharacters();
  public Task ImportCharacter(ApiCharacter character);

  public Task AddAlias(Character character, string alias);
  public Task<Character?> FindByName(string name);

  public List<Character> FindByAlias(string alias, int page);

}
