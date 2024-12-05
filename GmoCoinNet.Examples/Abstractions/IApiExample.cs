namespace GmoCoinNet.Examples.Abstractions;

public interface IApiExample
{
    string Name { get; }
    string Description { get; }
    Task RunAsync();
} 