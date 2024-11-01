// Verificar https://aka.ms/new-console-template para mais informações
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
using var conn = connectionFactory.CreateConnection();
using var model = conn.CreateModel();
const string queueName = "fila_teste";

model.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var eventConsumer = new EventingBasicConsumer(model);
eventConsumer.Received += (sender, ea) =>
{
    var payload = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(payload);
    Console.WriteLine($"Atualização de estoque recebida: {message}");
};

model.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: eventConsumer
);

Console.WriteLine("Aguardando mensagens...");
Console.ReadLine();
