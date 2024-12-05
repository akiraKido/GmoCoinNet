using GmoCoinNet.Examples.Abstractions;
using GmoCoinNet.Examples.Examples;

namespace GmoCoinNet.Examples;

public class Program
{
    private static readonly GmoCoinPublicApi PublicApi = new();
    private static readonly GmoCoinPublicWebSocket WebSocket = new();
    private static readonly IApiExample[] Examples =
    [
        new ServiceStatusExample(PublicApi),
        new BtcTickerExample(PublicApi),
        new BtcTickerStreamExample(WebSocket),
        new BtcOrderBookStreamExample(WebSocket)
    ];

    public static async Task Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("GMO Coin API Examples");
            Console.WriteLine("--------------------");
            
            for (var i = 0; i < Examples.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Examples[i].Name}");
            }
            Console.WriteLine($"{Examples.Length + 1}. Exit");
            
            Console.Write($"\nSelect an option (1-{Examples.Length + 1}): ");

            if (!int.TryParse(Console.ReadLine(), out var choice) || choice < 1 || choice > Examples.Length + 1)
            {
                continue;
            }

            Console.Clear();
            try
            {
                if (choice <= Examples.Length)
                {
                    var example = Examples[choice - 1];
                    Console.WriteLine($"{example.Name}");
                    Console.WriteLine($"{new string('-', example.Name.Length)}");
                    Console.WriteLine($"{example.Description}\n");
                    await example.RunAsync();
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (choice <= Examples.Length)
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}
