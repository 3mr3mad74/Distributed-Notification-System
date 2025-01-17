using Application.Contracts;
using Application.Services;
using Infrastructure.SignalR;
using Infrastructure.Repository;
using Infrastructure.RabbitMQ;
using Application.Models;
using Serilog;
using Microsoft.Extensions.Hosting;


namespace Notification_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .WithOrigins("http://127.0.0.1:5500")  
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); 
            });
            builder.Services.AddSignalR();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<INotifactionSignalRRepository, NotifactionSignalRRepository>();
            builder.Services.AddScoped<INotificationDatabaseRepository, NotificationDatabaseRepository>();
            builder.Services.AddScoped<NotificationTypeFactory>();
            builder.Services.AddHostedService<NotificationListener>();
            builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("D:/NotifiactionLogs.txt")  
                .CreateLogger();

            builder.Host.UseSerilog();



            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            app.UseAuthorization();
       
            app.MapHub<NotificationHub>("/notificationHub");

            app.MapControllers();
          
            
            app.Run("http://localhost:5000"); 
        }
    }
}
