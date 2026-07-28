using App.Bot;
using App.Commands;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var token = configuration["TelegramBot:Token"];

        if (!string.IsNullOrEmpty(token))
        {
            services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(token));
        }

        // Commands and Routing
        services.AddScoped<ICommand, StartCommand>();
        services.AddScoped<ICommand, PriceCommand>();    
        services.AddScoped<CommandRouter>();

        // Bot
        services.AddScoped<UpdateHandler>();
        services.AddSingleton<ErrorHandler>();

        // API
        services.AddHttpClient();
    })
    .Build();


var config = host.Services.GetRequiredService<IConfiguration>();

var botClient = host.Services.GetService<ITelegramBotClient>();

if (botClient == null)
{
    Console.WriteLine("Telegram bot token is missing or client is disabled.");
    return;
}

var errorHandler = host.Services.GetRequiredService<ErrorHandler>();
var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new[] { UpdateType.Message },
    DropPendingUpdates = true,
};


botClient.StartReceiving(
    updateHandler: async (client, update, cancellationToken) =>
    {
        using var messageScope = host.Services.CreateScope();
        var handler = messageScope.ServiceProvider.GetRequiredService<UpdateHandler>();

        await handler.HandleMessage(client, update, cancellationToken);
    },
    errorHandler: async (client, exception, cancellationToken) => 
    {
        await errorHandler.HandleError(client, exception, cancellationToken);
    },
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

Console.WriteLine("Bot is running! Press Ctrl+C to exit.");

await Task.Delay(-1);