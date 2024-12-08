using System.Text.Json.Serialization;

namespace GoTApiDas.Models.Entities;

public class CharacterAlias
{
  public int Id { get; init; }
  public required string Alias { get; init; }

  public int CharacterId { get; init; }

  [JsonIgnore]
  public Character? Character { get; init; }

}
