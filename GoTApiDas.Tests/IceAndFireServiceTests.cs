using GoTApiDas.Models.Entities;
using GoTApiDas.Models.IceOfFireApi;
using GoTApiDas.Producers;
using GoTApiDas.Repositories;
using GoTApiDas.Services.IceAndFire;
using Moq;

namespace GotApiDas.Tests;

public class IceAndFireServiceTests
{
  
  [Fact]
  public async Task ShouldImportCharacters()
  {
    var mockedCharacter = new ApiCharacter() { Name = "Test character", Aliases = ["Test alias"] };
    
    var httpClient = new Mock<IIceAndFireHttpClient>();
    httpClient.Setup(client => client.FetchCharacters())
      .Returns(Task.FromResult(new List<ApiCharacter>(){ mockedCharacter })!);
    
    var producer = new Mock<IBasicProducer>();
    var repository = new Mock<ICharacterRepository>();
    
    var iceAndFireService = new IceAndFireService(repository.Object, producer.Object, httpClient.Object);
    await iceAndFireService.ImportCharacters();
    
    repository.Verify(repo => repo.ImportCharacter(mockedCharacter), Times.Once);
    producer.Verify(p => p.QueueMsg($"Character {mockedCharacter.Name} imported"), Times.Once);
  }
  
  [Fact]
  public async Task ShouldNotImportExistingCharacters()
  {
    var mockedCharacter = new ApiCharacter() { Name = "Test character", Aliases = ["Test alias"] };
    
    var httpClient = new Mock<IIceAndFireHttpClient>();
    httpClient.Setup(client => client.FetchCharacters())
      .Returns(Task.FromResult(new List<ApiCharacter>(){ mockedCharacter })!);
    
    var producer = new Mock<IBasicProducer>();
    var repository = new Mock<ICharacterRepository>();
    repository.Setup(repo => repo.FindByName(mockedCharacter.Name))
      .Returns(Task.FromResult(new Character()
      {
        Id = 1, 
        Name = mockedCharacter.Name,
        Born = mockedCharacter.Born,
        Culture = mockedCharacter.Culture,
        Died = mockedCharacter.Died,
        Gender = mockedCharacter.Gender
      })!);
    
    var iceAndFireService = new IceAndFireService(repository.Object, producer.Object, httpClient.Object);
    await iceAndFireService.ImportCharacters();
    
    repository.Verify(repo => repo.ImportCharacter(mockedCharacter), Times.Never);
    repository.Verify(repo => repo.FindByName(mockedCharacter.Name), Times.Once);
    producer.Verify(p => p.QueueMsg($"Character {mockedCharacter.Name} imported"), Times.Never);
  }

}
