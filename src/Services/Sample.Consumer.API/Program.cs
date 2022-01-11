using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using RabbitMQ.Client;
using Sample.Consumer.API;
using Sample.Consumer.API.IntegrationEvents.EventHandling;
using Sample.Consumer.API.IntegrationEvents.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

const int retryCount = 5;
builder.Services
    .AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>()
    .AddTransient<OrderStatusChangedIntegrationEventHandler>()
    .AddSingleton<IRabbitMQPersistentConnection>(sp =>
    {
        var rabbitMQConf = builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
        var factory = new ConnectionFactory()
        {
            DispatchConsumersAsync = true,
            HostName = rabbitMQConf.RABBITMQ_HOSTNAME,//localhost, rabbitmq
            UserName = rabbitMQConf.RABBITMQ_DEFAULT_USER,
            Password = rabbitMQConf.RABBITMQ_DEFAULT_PASS
        };

        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    })
    .AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    {
        var rabbitMQConf = builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        var subscriptionClientName = rabbitMQConf.RABBITMQ_SUBSCRIPTION_CLIENTNAME;
        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    })
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Sample Consumer API", Version = "v1" });
    })
    .AddLogging(builder =>
    {
        var serilogLogger = new LoggerConfiguration().WriteTo.File("Logs/Log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddSerilog(logger: serilogLogger, dispose: true);
    })
    .AddControllersWithViews()
    .AddControllersAsServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version V1"));
}

var eventBus = app.Services.GetRequiredService<IEventBus>();
//eventBus.Subscribe<OrderStatusChangedIntegrationEvent, OrderStatusChangedIntegrationEventHandler>();
eventBus.SubscribeForAttribute(typeof(Program));

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();