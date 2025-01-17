
using Application.Contracts;
using Application.Models;
using Application.Services;
using Infrastructure.Repository;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;


namespace Infrastructure.RabbitMQ
{
    public class NotificationListener : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationListener(IConfiguration configuration, IHubContext<NotificationHub> hubContext)
        {
            _configuration = configuration;
            _queueName = _configuration.GetSection("RabbitMQ:queueName").Value;
            _hubContext = hubContext;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                stoppingToken.Register(() =>
                {
                    Log.Information("Notification service is stopping.");
                    _channel?.Close();
                    _connection?.Close();
                    Log.Information("RabbitMQ channel and connection closed.");

                });
                Log.Information("Notification services start execution ...");
                var URL = _configuration.GetSection("RabbitMQ:URI").Value;
                ConnectionFactory connectionFactory = new ConnectionFactory();
                connectionFactory.Uri = new Uri(URL);
                Log.Information("Creating RabbitMQ connection to {URL}.", URL);
                _connection = connectionFactory.CreateConnection();
                Log.Information("RabbitMQ connection established.");
                Log.Information("Creating RabbitMQ channel.");
                _channel = _connection.CreateModel();
                Log.Information("RabbitMQ channel created.");
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Log.Information("Received message from queue {QueueName}: {Message}", _queueName, message);
                    ProcessMessage(message, ea.DeliveryTag);
                };
                _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
               
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               Log.Error(ex, "An error occurred during the execution of the notification service.");
            }
        }
        private void ProcessMessage(string message, ulong deliveryTag)
        {
            try
            {
                var jsonMessage = JObject.Parse(message);
                var type = jsonMessage["Type"].ToString();
                Log.Information("Processing message of type {Type}.", type);

                var notifactionRepository = new NotifactionSignalRRepository(_hubContext);

                var notifactionDatabaseRepository = new NotificationDatabaseRepository();
                var notificationTypeFactory = new NotificationTypeFactory(notifactionRepository , notifactionDatabaseRepository);
                var notifactionService = new NotificationService(notificationTypeFactory, notifactionRepository);

                Log.Information("Handling notification message of type {Type}.", type);
                notifactionService.HandleNotificationMessage(type, message);
                Log.Information("Notification message handled successfully.");

                _channel.BasicAck(deliveryTag, multiple: false);
                Log.Information("Message acknowledged with delivery tag {DeliveryTag}.", deliveryTag);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the message: {Message}", message);
                _channel.BasicReject(deliveryTag, requeue: false);
            }
        }

       
    }
}

