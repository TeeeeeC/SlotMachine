namespace SlotMachine.Interfaces
{
    public interface IReport
    {
        char[,] Matrix { get; }

        decimal Profit { get; }

        decimal Balance { get; }
    }
}
