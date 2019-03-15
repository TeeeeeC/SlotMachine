namespace SlotMachine.Objects
{
    using SlotMachine.Interfaces;
    using System;

    public class ConsoleMessageReader : IMessageReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
