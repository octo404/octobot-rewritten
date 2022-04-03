

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
            await _client.SetGameAsync("octobot-rewritten ~help");
            
        
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
            string prefix = "~";

            var input = arg.Content;
            var output_channel = arg.Channel;
            var output_user = arg.Author;

            if (input.StartsWith(prefix + "hello"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'hello'");
                output_channel.SendMessageAsync($"Hello '{arg.Author.Mention}'!");
            }

            if(input.StartsWith(prefix + "ping"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'ping'");

                output_channel.SendMessageAsync("Pong.");
            }
            
            if(input.StartsWith(prefix + "help"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'help'");
                string helptext = File.ReadAllText("./help.txt");
                output_channel.SendMessageAsync($"Hey {arg.Author.Mention} look at your DM's!");
                output_user.SendMessageAsync(helptext);

            }
            //ownercommands
            
            if(arg.Author.Id == 375639304577482755 && input.StartsWith(prefix + "shutdown"))
            {
                Console.WriteLine($"Log: {arg.Author.Username}: used command 'shutdown'");
                output_channel.SendMessageAsync("Shuting down...");
                //legit hard kill it xD
                Environment.Exit(0);
                
            }
            return Task.CompletedTask;
        }
    }
}