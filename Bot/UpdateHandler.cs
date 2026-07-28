using App.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace App.Bot;

public class UpdateHandler
{
    private readonly CommandRouter _router;

    public UpdateHandler(CommandRouter router)
    {
        _router = router;
    }

    public async Task HandleMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Type != UpdateType.Message || update.Message?.Text is not string text)
                return;

            Console.WriteLine($"Пришло сообщение: {text}");
            text = text.Trim();

            string command;
            string[] parts;

            if (text.StartsWith('/'))
            {
                parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                command = parts[0];
            }
            else
            {
                command = text;
                parts = new[] { text };
            }

            await _router.Execute(command, parts, botClient, update, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bot Error: {ex}");
        }
    }
}