# GmoCoinNet

GmoCoinNet is a .NET client library for interacting with the GMO Coin cryptocurrency exchange API. It provides both REST API and WebSocket implementations for accessing public and private endpoints.

## Features

- Public API support
  - Order book data
  - Trade history
  - Market statistics
  - Trading rules
  - Candlestick/KLine data
- WebSocket support for real-time data
  - Order book updates
  - Trade updates
  - Ticker updates

## Installation

Install via NuGet Package Manager:

    dotnet add package GmoCoinNet

## Quick Start

### Public API Example

    using GmoCoinNet;

    // Create public API client
    var publicApi = new GmoCoinPublicApi();

    // Get order book
    var orderBook = await publicApi.GetOrderBookAsync("BTC_JPY");

    // Get recent trades
    var trades = await publicApi.GetTradesAsync("BTC_JPY");

    // Get trading rules
    var rules = await publicApi.GetTradeRulesAsync();

### WebSocket Example

    using GmoCoinNet;

    // Create WebSocket client
    var ws = new GmoCoinPublicWebSocket();

    // Subscribe to order book updates
    ws.SubscribeToOrderBook("BTC_JPY");

    // Handle order book updates
    ws.OnOrderBookUpdate += (sender, update) => 
    {
        Console.WriteLine($"Ask Price: {update.Ask[0].Price}");
        Console.WriteLine($"Bid Price: {update.Bid[0].Price}");
    };

    // Connect to WebSocket
    await ws.ConnectAsync();

## Documentation

For detailed API documentation and examples, please refer to the [GMO Coin API Documentation](https://api.coin.z.com/docs/).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Disclaimer

This library is not officially associated with GMO Coin. Use at your own risk.
