using GoTApiDas.Models.Entities;
using GoTApiDas.Repositories;
using GoTApiDas.Services.IceAndFire;
using Microsoft.AspNetCore.Mvc;

namespace GoTApiDas.Controllers;

[Route("/Api")]
public class ApiController(ICharacterRepository characterRepository, IIceAndFireService iceAndFireService)
  : Controller
{
  
  [HttpGet("Characters")]
  public IActionResult Characters()
  {
    return Ok(characterRepository.GetCharacters());
  }

  [HttpGet("FindByAlias")]
  public IActionResult FindCharacterByAlias([FromQuery] string alias, [FromQuery] int page = 0)
  {
    var result = characterRepository.FindByAlias(alias, page);
    if (result.Count > 0)
    {
      return Ok(result);
    }
    else
    {
      return NotFound($"No character(s) with alias '{alias}' found.");
    }
  }

  [HttpPost("Characters")]
  public async Task<IActionResult> CreateCharacter([FromBody] Character character)
  {
    await characterRepository.AddCharacter(character);
    return Ok();
  }

  [HttpPost("ImportCharacters")]
  public async Task<IActionResult> ImportCharacters()
  {
    try
    {
      await iceAndFireService.ImportCharacters();
      return Ok();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
