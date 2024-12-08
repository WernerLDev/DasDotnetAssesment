namespace GoTApiDas.Models.IceOfFireApi;

public class ApiCharacter
{

  public string Url { get; set; } = "";
  public string Name { get; set; } = "";
  public string Gender { get; set; } = "";
  public string Culture { get; set; } = "";
  public string Born { get; set; } = "";
  public string Died { get; set; } = "";
  public IEnumerable<string> Titles { get; set; } = [];
  public IEnumerable<string> Aliases { get; init; } = [];
  public string Father { get; set; } = "";
  public string Mother { get; set; } = "";
  public string Spouse { get; set; } = "";
  public IEnumerable<string> Allegiances { get; set; } = [];

}
