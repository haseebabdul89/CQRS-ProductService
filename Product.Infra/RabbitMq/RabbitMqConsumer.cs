using MediatR;
using Microsoft.Extensions.Configuration;
using Product.Application.Commands;
using Product.Application.Interfaces;
using Product.Application.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Product.Infra.RabbitMq
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMediator _mediator;
        private readonly string _queueName;

        public RabbitMqConsumer(IConfiguration config, IMediator mediator)
        {
            _mediator = mediator;
            _queueName = config["RabbitMQ:ShoppingCartQueue"] ?? "shopping_cart";

            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMQ:Host"] ?? "localhost",
                UserName = config["RabbitMQ:Username"] ?? "guest",
                Password = config["RabbitMQ:Password"] ?? "guest",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);

                // Deserialize to ProductInventoryMessage
                var message = JsonSerializer.Deserialize<ProductInventoryMessage>(messageJson);

                if (message != null)
                {
                    // Send to MediatR command
                    await _mediator.Send(new UpdateProductInventoryCommand(
                        message.ProductId, message.Quantity, message.Reason));
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
    }
}
