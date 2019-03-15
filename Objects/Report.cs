namespace SlotMachine.Objects
{
    using SlotMachine.Interfaces;

    public class Report : IReport
    {
        public char[,] Matrix { get; set; }

        public decimal Profit { get; set; }

        public decimal Balance { get; set; }
    }
}
