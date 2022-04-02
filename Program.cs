

using Discord;

using Discord.API;
using Discord.Net;
using Discord.Audio;
using Discord.Commands;
using Discord.Interactions;
using Discord.Rest;
using Discord.Webhook;

using Discord.WebSocket;

namespace octobot_rewritten
{
    class Program
    {
        
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += ClientOnMessageReceived;
            

            var token = File.ReadAllText("./token.txt");
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetGameAsync("octobot-rewritten o!help");
            
        
            // Block this task until the program is closed.
            await Task.Delay(-1);
            
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private static Task ClientOnMessageReceived(SocketMessage arg)
        {
            string prefix = "o!";

            var input = arg.Content;
            var output = arg.Channel;

            if (input.StartsWith(prefix + "hello"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'hello'");
                output.SendMessageAsync($"Hello '{arg.Author.Username}'!");
            }

            if(input.StartsWith(prefix + "ping"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'ping'");

                output.SendMessageAsync("Pong.");
            }

            if(input.StartsWith(prefix + "help"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'help'");
                output.SendMessageAsync("still working...");

            }
            return Task.CompletedTask;
        }
    }
}