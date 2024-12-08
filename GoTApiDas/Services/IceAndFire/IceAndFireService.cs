using GoTApiDas.Producers;
using GoTApiDas.Repositories;

namespace GoTApiDas.Services.IceAndFire;

public class IceAndFireService(
  ICharacterRepository characterRepo,
  IBasicProducer producer,
  IIceAndFireHttpClient client)
  : IIceAndFireService
{
  public async Task ImportCharacters()
  {
    var characters = await client.FetchCharacters();
    characters?.ForEach(async void (character) =>
    {
      if (await characterRepo.FindByName(character.Name) != null) return;
      await characterRepo.ImportCharacter(character);
      await producer.QueueMsg($"Character {character.Name} imported");
    });
  }
}
