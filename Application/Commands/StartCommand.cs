using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace App.Commands;

public class StartCommand : ICommand
{
    public string Name => "/start";

    public bool CanExecute(string commandText) => 
        commandText.Equals("/start", StringComparison.OrdinalIgnoreCase);

    public async Task Execute(ITelegramBotClient bot, Update update, CancellationToken token, string[] args)
    {
        if (update.Message is null) return;

        string text = "👋 *Привет! Я твой асинхронный крипто-ассистент.*\n\n" +
                      "Используй меню внизу, чтобы мгновенно узнать актуальный курс монет:";


        var replyKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[] { "🪙 Bitcoin", "🔷 Ethereum" },
            new KeyboardButton[] { "💎 Toncoin" }
        })
        {
            ResizeKeyboard = true,
            IsPersistent = true 
        };

        await bot.SendMessage(
            chatId: update.Message.Chat.Id,
            text: text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
            replyMarkup: replyKeyboard,
            cancellationToken: token
        );
    }
}
