namespace SlotMachine.Objects.Symbols
{
    using SlotMachine.Interfaces;

    public abstract class Symbol : ISymbol
    {
        public Symbol(char name)
        {
            this.Name = name;
        }

        public char Name { get; private set; }

        public abstract decimal Coefficient { get; set; }

        public abstract int ProbabilityToApearOnCell { get; set; }
    }
}
