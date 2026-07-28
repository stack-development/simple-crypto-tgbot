using Telegram.Bot;
using Telegram.Bot.Types;

namespace App.Commands;

public interface ICommand
{
    public string Name { get; }
    public bool CanExecute(string commandText); 
    public Task Execute(ITelegramBotClient bot, Update update, CancellationToken token, string[] args);
}
