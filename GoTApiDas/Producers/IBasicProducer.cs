namespace GoTApiDas.Producers;

public interface IBasicProducer
{
  public Task QueueMsg(string message);
}
