using RabbitMQ.Client;
using System.Text;

namespace GoTApiDas.Producers;

public class BasicProducer : IBasicProducer
{
  public async Task QueueMsg(string message)
  {
    var factory = new ConnectionFactory { HostName = "localhost" };
    await using var connection = await factory.CreateConnectionAsync();
    await using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(queue: "task_queue", durable: true, exclusive: false,
        autoDelete: false, arguments: null);

    var body = Encoding.UTF8.GetBytes(message);

    var properties = new BasicProperties
    {
      Persistent = true
    };

    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "task_queue", mandatory: true,
        basicProperties: properties, body: body);
    Console.WriteLine($" [x] Sent {message}");
  }
}
