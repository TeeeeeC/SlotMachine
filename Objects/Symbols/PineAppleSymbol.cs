﻿namespace SlotMachine.Objects.Symbols
{
    public class PineAppleSymbol : Symbol
    {
        public PineAppleSymbol(char name, decimal coefficient, int probabilityToApearOnCell)
            : base(name)
        {
            this.Coefficient = coefficient;
            this.ProbabilityToApearOnCell = probabilityToApearOnCell;
        }

        public override decimal Coefficient { get; set; }

        public override int ProbabilityToApearOnCell { get; set; }
    }
}