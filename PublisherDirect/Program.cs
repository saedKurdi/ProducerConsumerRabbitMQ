using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://zuduguoa:i3rDng9dw_Zfoa29DkCTQPr04FeDoexR@ostrich.lmq.cloudamqp.com/zuduguoa");
using var connection = await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync("mystepqueue", durable: true, exclusive: false, autoDelete: false); // true burda onu bildirir ki, nese problem error hallari olanda restart verir <--- 

while (true)
{
    Console.Write("Enter your message for publishing : ");
    var message = string.Concat("Message - ",Console.ReadLine(), " Time - ", DateTime.Now.ToLongTimeString());
    var body = Encoding.UTF8.GetBytes(message);
    await channel.BasicPublishAsync("", "mystepqueue", body);
    await Console.Out.WriteLineAsync("Message sent succesfully .");
    Console.ReadLine();
}