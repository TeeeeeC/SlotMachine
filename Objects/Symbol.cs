namespace SlotMachine.Objects
{
    using SlotMachine.Interfaces;

    public class Symbol : ISymbol
    {
        public Symbol(char name, decimal coefficient, int probabilityToApearOnCell)
        {
            this.Name = name;
            this.Coefficient = coefficient;
            this.ProbabilityToApearOnCell = probabilityToApearOnCell;
        }

        public char Name { get; private set; }

        public decimal Coefficient { get; private set; }

        public int ProbabilityToApearOnCell { get; private set; }
    }
}
