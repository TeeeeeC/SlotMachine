namespace SlotMachine.Objects.Symbols
{
    public class AppleSymbol : Symbol
    {
        public AppleSymbol(char name, decimal coefficient, int probabilityToApearOnCell) 
            : base(name)
        {
            this.Coefficient = coefficient;
            this.ProbabilityToApearOnCell = probabilityToApearOnCell;
        }

        public override decimal Coefficient { get; set; }

        public override int ProbabilityToApearOnCell { get; set; }
    }
}
