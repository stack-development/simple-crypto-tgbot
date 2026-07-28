using Telegram.Bot;
using Telegram.Bot.Types;

namespace App.Commands;

public class CommandRouter
{
    private readonly IEnumerable<ICommand> _commands;

    public CommandRouter(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    public async Task Execute(
        string command,
        string[] args,
        ITelegramBotClient bot,
        Update update,
        CancellationToken token)
    {
        var cleanCommand = command.Split('@')[0].Trim();

        var handler = _commands.FirstOrDefault(x => x.CanExecute(cleanCommand) || 
                                                    x.Name.Equals(cleanCommand, StringComparison.OrdinalIgnoreCase));

        if (handler != null)
        {
            await handler.Execute(bot, update, token, args);
        }
        else
        {
            Console.WriteLine($"Command not found in router: {cleanCommand}");
        }
    }
}
