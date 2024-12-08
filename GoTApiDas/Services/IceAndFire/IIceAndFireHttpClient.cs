using GoTApiDas.Models.IceOfFireApi;

namespace GoTApiDas.Services.IceAndFire;

public interface IIceAndFireHttpClient
{
  public Task<List<ApiCharacter>?> FetchCharacters();
}
