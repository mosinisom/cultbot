using Telegram.Bot;
using Telegram.Bot.Types;

public class CommandHandler
{
    private MovieService _movieService = new MovieService();

    public async Task<Message> HandleCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        var messageText = message.Text;
        if (messageText is null)
        {
            return await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Please send a text message.",
            cancellationToken: cancellationToken);
        }

        // Here you can add more commands and their handling
        if (messageText.StartsWith("/start"))
        {
            return await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Welcome to our bot!",
                cancellationToken: cancellationToken);
        }
        else if (messageText.StartsWith("/help"))
        {
            return await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Here is a list of commands you can use...",
                cancellationToken: cancellationToken);
        }
        else if (messageText.StartsWith("/movies"))
        {
            var era = messageText.Split(' ').Length > 1 ? messageText.Split(' ')[1] : null;
            if (era is null)
            {
                return await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Please specify an era. For example: /movies 20th",
                    cancellationToken: cancellationToken);
            }

            var movies = _movieService.GetMoviesByEra(era);
            if (movies.Count == 0)
            {
                return await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"No movies found for the {era} era.",
                    cancellationToken: cancellationToken);
            }

            var responseText = $"Movies from the {era} era:\n\n";
            foreach (var movie in movies)
            {
                responseText += $"Title: {movie.Title}\nDescription: {movie.Description}\nCostume Designer: {movie.CostumeDesigner}\nLink: {movie.Link}\n\n";
            }

            return await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: responseText,
                cancellationToken: cancellationToken);
        }

        // If no command matched, just echo the message
        return await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
    }
}