namespace SlotMachine.Objects
{
    using SlotMachine.Interfaces;
    using System;

    public class ConsoleMessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
