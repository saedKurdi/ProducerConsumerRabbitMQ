using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://zuduguoa:i3rDng9dw_Zfoa29DkCTQPr04FeDoexR@ostrich.lmq.cloudamqp.com/zuduguoa");
using var connection = await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync("mystepqueue", durable: true, exclusive: false, autoDelete: false); // true burda onu bildirir ki, nese problem error hallari olanda restart verir <--- 

var consumer = new AsyncEventingBasicConsumer(channel);
await channel.BasicConsumeAsync("mystepqueue",true,consumer);

consumer.ReceivedAsync += Consumer_ReceivedAsync;

Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs @event)
{
    var message = Encoding.UTF8.GetString(@event.Body.ToArray());
    Console.WriteLine(message);
    return Task.CompletedTask;
}

Console.ReadLine();