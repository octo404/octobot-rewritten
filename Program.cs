

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
            var channel = arg.Channel;
            var user = arg.Author;

            double ownerid = Convert.ToDouble(File.ReadAllText("./ownerid.txt"));


            if (input.StartsWith(prefix + "hello"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'hello'");
                channel.SendMessageAsync($"Hello '{user.Mention}'!");
            }

            if(input.StartsWith(prefix + "ping"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'ping'");

                channel.SendMessageAsync("Pong.");
            }
            
            if(input.StartsWith(prefix + "help"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'help'");
                string helpfile = File.ReadAllText("./help.txt");
                channel.SendMessageAsync($"Hey {user.Mention} look at your DM's!");
                user.SendMessageAsync(helpfile);

            }
            
            if(input.StartsWith(prefix + "rng"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'rng'");
                Random r = new Random();
                channel.SendMessageAsync($"Your generated number is: **{r.Next(1, 100)}**");
                
            }
            /*
                TODO: RNG with custom number input
            */

            //ownercommands
            if(arg.Author.Id == ownerid && input.StartsWith(prefix + "ownerhelp"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'ownerhelp'");
                string ownerhelp_file = File.ReadAllText("./ownerhelp.txt");
                user.SendMessageAsync(ownerhelp_file);
            }

            if(arg.Author.Id == ownerid && input.StartsWith(prefix + "shutdown"))
            {
                Console.WriteLine($"Log: {user.Username}: used command 'shutdown'");
                channel.SendMessageAsync("Shuting down...");
                //legit hard kill it 
                Environment.Exit(0);
                
            }
            return Task.CompletedTask;
        }
    }
}