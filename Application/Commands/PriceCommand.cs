using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace App.Commands;

public class BinanceTicker
{
    [JsonPropertyName("price")]
    public string Price { get; set; } = string.Empty;
}

public class PriceCommand : ICommand
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public string Name => "Crypto Price Command"; 

    public PriceCommand(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public bool CanExecute(string commandText)
    {
        return commandText == "🪙 Bitcoin" || 
               commandText == "🔷 Ethereum" || 
               commandText == "💎 Toncoin";
    }

    public async Task Execute(ITelegramBotClient bot, Update update, CancellationToken token, string[] args)
    {
        if (update.Message is null || string.IsNullOrEmpty(args[0])) return;

        string userRequest = args[0];

        string symbol = userRequest switch
        {
            "🪙 Bitcoin" => "BTCUSDT",
            "🔷 Ethereum" => "ETHUSDT",
            "💎 Toncoin" => "TONUSDT",
            _ => ""
        };

        if (string.IsNullOrEmpty(symbol)) return;

        var statusMessage = await bot.SendMessage(update.Message.Chat.Id, "🔄 _Getting data from API..._", 
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, cancellationToken: token);

        try
        {
            var client = _httpClientFactory.CreateClient();

            var url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";
            var ticker = await client.GetFromJsonAsync<BinanceTicker>(url, cancellationToken: token);

            if (ticker != null && double.TryParse(ticker.Price, out double rawPrice))
            {
                string formattedPrice = rawPrice > 1 ? rawPrice.ToString("N2") : rawPrice.ToString("N4");
                
                string responseText = $"📊 *Exchange rate {userRequest} on Binance:*\n\n" +
                                      $"💵 *Price:* `{formattedPrice} USDT`";

                await bot.EditMessageText(
                    chatId: update.Message.Chat.Id,
                    messageId: statusMessage.MessageId,
                    text: responseText,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    cancellationToken: token
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Error: {ex}");
            await bot.EditMessageText(update.Message.Chat.Id, statusMessage.MessageId, "❌ _Cannot get values from Binance API._", 
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, cancellationToken: token);
        }
    }
}
