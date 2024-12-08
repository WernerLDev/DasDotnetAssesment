namespace GoTApiDas.Models.Entities;

public class Character
{
  public int Id { get; set; }
  public required string Name { get; init; }
  public required string Gender { get; init; }

  public required string Culture { get; init; }

  public required string Born { get; init; }

  public required string Died { get; init; }

  public ICollection<CharacterAlias> CharacterAliases { get; set; } = new List<CharacterAlias>();
}

