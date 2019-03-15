namespace SlotMachine.Interfaces
{
    public interface ISymbol
    {
        char Name { get; }

        decimal Coefficient { get; }

        int ProbabilityToApearOnCell { get; }
    }
}
