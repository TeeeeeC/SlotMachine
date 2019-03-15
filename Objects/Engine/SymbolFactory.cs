namespace SlotMachine.Objects.Engine
{
    using SlotMachine.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class SymbolFactory : ISymbolFactory
    {
        private const char SYMBOL_APPLE = 'A';
        private const char SYMBOL_BANANA = 'B';
        private const char SYMBOL_PINEAPPLE = 'P';
        private const char SYMBOL_WILDCARD = '*';

        public SymbolFactory()
        {
            this.AppleSymbol = this.GetSymbol(SYMBOL_APPLE, 0.4M, 45);
            this.BananaSymbol = this.GetSymbol(SYMBOL_BANANA, 0.6M, 35);
            this.PineAppleSymbol = this.GetSymbol(SYMBOL_PINEAPPLE, 0.8M, 15);
            this.WildcardSymbol = this.GetSymbol(SYMBOL_WILDCARD, 0, 5);
            this.Symbols = new Dictionary<char, ISymbol>();
            this.Symbols.Add(SYMBOL_APPLE, this.AppleSymbol);
            this.Symbols.Add(SYMBOL_BANANA, this.BananaSymbol);
            this.Symbols.Add(SYMBOL_PINEAPPLE, this.PineAppleSymbol);
            this.Symbols.Add(SYMBOL_WILDCARD, this.WildcardSymbol);
        }

        public ISymbol AppleSymbol { get;  private set; }

        public ISymbol BananaSymbol { get; private set; }

        public ISymbol PineAppleSymbol { get; private set; }

        public ISymbol WildcardSymbol { get; private set; }

        public Dictionary<char, ISymbol> Symbols { get; private set; }

        public List<char> GetSymbols()
        {
            var symbols = new List<char>();

            symbols.AddRange(Enumerable.Repeat(this.AppleSymbol.Name, this.AppleSymbol.ProbabilityToApearOnCell));
            symbols.AddRange(Enumerable.Repeat(this.BananaSymbol.Name, this.BananaSymbol.ProbabilityToApearOnCell));
            symbols.AddRange(Enumerable.Repeat(this.PineAppleSymbol.Name, this.PineAppleSymbol.ProbabilityToApearOnCell));
            symbols.AddRange(Enumerable.Repeat(this.WildcardSymbol.Name, this.WildcardSymbol.ProbabilityToApearOnCell));

            return symbols;
        }

        private ISymbol GetSymbol(char name, decimal coefficient, int probability)
        {
            return new Symbol(name, coefficient, probability);
        }
    }
}
