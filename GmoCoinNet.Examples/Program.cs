using GmoCoinNet;
using GmoCoinNet.Schema;

namespace GmoCoinNet.Examples;

public class Program
{
    private static readonly GmoCoinPublicApi PublicApi = new();
    private static readonly GmoCoinPublicWebSocket WebSocket = new();

    public static async Task Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("GMO Coin API Examples");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Get Service Status");
            Console.WriteLine("2. Get BTC Ticker");
            Console.WriteLine("3. Subscribe to BTC Ticker Updates");
            Console.WriteLine("4. Subscribe to BTC Order Book Updates");
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an option (1-5): ");

            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                continue;
            }

            Console.Clear();
            try
            {
                await RunExample(choice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (choice == 5) break;
            
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }

    private static async Task RunExample(int choice)
    {
        switch (choice)
        {
            case 1:
                await ShowServiceStatus();
                break;
            case 2:
                await ShowBtcTicker();
                break;
            case 3:
                await SubscribeToBtcTicker();
                break;
            case 4:
                await SubscribeToBtcOrderBook();
                break;
            case 5:
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    private static async Task ShowServiceStatus()
    {
        Console.WriteLine("Getting service status...\n");
        var status = await PublicApi.GetStatusAsync();
        Console.WriteLine($"GMO Coin service status: {status.Data.Status}");
    }

    private static async Task ShowBtcTicker()
    {
        Console.WriteLine("Getting BTC ticker...\n");
        var ticker = await PublicApi.GetTickerAsync(Ticker.Btc);
        var bitcoinData = ticker.Data[0];
        Console.WriteLine($"BTC price: Ask={bitcoinData.Ask}, Bid={bitcoinData.Bid}");
    }

    private static async Task SubscribeToBtcTicker()
    {
        Console.WriteLine("Subscribing to BTC ticker updates (press any key to stop)...\n");
        
        using var cancellationTokenSource = new CancellationTokenSource();
        var subscription = Task.Run(async () =>
        {
            await foreach (var update in WebSocket.SubscribeToTickerStream(Ticker.Btc, cancellationTokenSource.Token))
            {
                Console.WriteLine($"BTC update: Ask={update.Ask}, Bid={update.Bid}, Time={update.Timestamp:HH:mm:ss.fff}");
            }
        });

        Console.ReadKey(true);
        await cancellationTokenSource.CancelAsync();
        await subscription;
    }

    private static async Task SubscribeToBtcOrderBook()
    {
        Console.WriteLine("Subscribing to BTC order book updates (press any key to stop)...\n");
        
        using var cancellationTokenSource = new CancellationTokenSource();
        var subscription = Task.Run(async () =>
        {
            await foreach (var update in WebSocket.SubscribeToOrderBooks(Ticker.Btc, cancellationTokenSource.Token))
            {
                Console.WriteLine($"Order Book update at {update.Timestamp:HH:mm:ss.fff}:");
                Console.WriteLine($"Best Ask: {update.Asks[0].Price} ({update.Asks[0].Size})");
                Console.WriteLine($"Best Bid: {update.Bids[0].Price} ({update.Bids[0].Size})");
                Console.WriteLine();
            }
        });

        Console.ReadKey(true);
        await cancellationTokenSource.CancelAsync();
        await subscription;
    }
}
