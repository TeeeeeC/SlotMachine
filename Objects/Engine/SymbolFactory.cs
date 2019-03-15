namespace SlotMachine.Objects.Engine
{
    using SlotMachine.Interfaces;
    using SlotMachine.Objects.Symbols;
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
            this.AppleSymbol = this.GetApple(SYMBOL_APPLE);
            this.BananaSymbol = this.GetBanana(SYMBOL_BANANA);
            this.PineAppleSymbol = this.GetPineApple(SYMBOL_PINEAPPLE);
            this.WildcardSymbol = this.GetWildcard(SYMBOL_WILDCARD);
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

        private ISymbol GetApple(char name)
        {
            return new AppleSymbol(name, 0.4M, 45);
        }

        private ISymbol GetBanana(char name)
        {
            return new BananaSymbol(name, 0.6M, 35);
        }

        private ISymbol GetPineApple(char name)
        {
            return new PineAppleSymbol(name, 0.8M, 15);
        }

        private ISymbol GetWildcard(char name)
        {
            return new WildcardSymbol(name, 0, 5);
        }
    }
}
