namespace SlotMachine.Interfaces
{
    using System.Collections.Generic;

    public interface ISymbolFactory
    {
        ISymbol AppleSymbol { get; }

        ISymbol BananaSymbol { get; }

        ISymbol PineAppleSymbol { get; }

        ISymbol WildcardSymbol { get; }

        Dictionary<char, ISymbol> Symbols { get; }

        List<char> GetSymbols();
    }
}
