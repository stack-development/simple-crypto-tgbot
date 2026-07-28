using Telegram.Bot;
using Telegram.Bot.Types;

namespace App.Bot;

class ErrorHandler
{
    public Task HandleError(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        Console.WriteLine(error);
        return Task.CompletedTask;
    }
}