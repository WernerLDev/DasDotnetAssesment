using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GoTApiDas.Workers;

public class BasicWorker
{
  public async void StartListening()
  {
    var factory = new ConnectionFactory { HostName = "localhost" };
    await using var connection = await factory.CreateConnectionAsync();
    await using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(queue: "task_queue", durable: true, exclusive: false,
    autoDelete: false, arguments: null);

    await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += async (model, ea) =>
    {
      byte[] body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Received {message}");

      int dots = message.Split('.').Length - 1;
      await Task.Delay(dots * 1000);

      Console.WriteLine(" [x] Done");

      // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
      await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
    };

    await channel.BasicConsumeAsync("task_queue", autoAck: false, consumer: consumer);

  }
}