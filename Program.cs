using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;


class Program
{
    // Coloque seu token do bot aqui
    private static readonly string BotToken = "7498766364:AAGizrCw9iLQ8YrRkaALkmoQ4Ny3BCDtRk0";
    private static TelegramBotClient botClient;

    static void Main(string[] args)
    {
        // Inicializa o cliente do bot com o token
        botClient = new TelegramBotClient(BotToken);

        // Configurações para o recebimento de mensagens
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // Recebe todos os tipos de atualizações
        };

        // Inicia o recebimento de atualizações
        botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions);

        Console.WriteLine("Bot iniciado. Pressione qualquer tecla para parar o bot...");
        Console.ReadKey();

        // Para o recebimento de mensagens
        botClient.CloseAsync().Wait();
    }

    // Método para processar as atualizações (mensagens)
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Apenas processa mensagens de texto
        if (update.Type == UpdateType.Message && update.Message!.Text != null)
        {
            var message = update.Message;

            Console.WriteLine($"Recebi uma mensagem de {message.Chat.FirstName}: {message.Text}");

            // Verificar perguntas sobre o Rio de Janeiro
            string resposta = ProcessarPerguntaSobreRio(message.Text.ToLower());

            // Enviar a resposta para o usuário
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: resposta
            );
        }
    }

    // Método para processar perguntas relacionadas ao Rio de Janeiro
    private static string ProcessarPerguntaSobreRio(string pergunta)
    {
        if (pergunta.Contains("cristo redentor"))
        {
            return "O Cristo Redentor é uma estátua de Jesus Cristo localizada no topo do Corcovado, no Rio de Janeiro.";
        }
        else if (pergunta.Contains("oi") ||
        pergunta.Contains("ola") ||
        pergunta.Contains("bom dia") ||
        pergunta.Contains("boa tarde") ||
        pergunta.Contains("boa noite") ||
        pergunta.Contains("e aí") ||
        pergunta.Contains("fala") ||
        pergunta.Contains("salve"))
        {
                return "Olá saudações!!!";
            }
        else if (pergunta.Contains("aquario"))
        {
            return "Depois de descobrir e de regular fatores como a temperatura e o pH da água do seu aquário, saiba que eles, eventualmente, irão se alterar. Isso ocorre porque todo aquário é um ecossistema. Nele, estão constantemente ocorrendo trocas físicas e químicas que acarretam mudanças nas condições da água. Sempre que isso acontece, é preciso fazer as correções com a ajuda de condicionadores (que, por exemplo, alcalinizam ou acidificam a água de acordo com a necessidade) ou de limpeza em conjunto com trocas parciais de água.";
        }
        else
        {
            return "Ih rapazzz, não sei nada sobre isso não";
        }
    }
    // Método para lidar com erros
    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Erro na API do Telegram:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
