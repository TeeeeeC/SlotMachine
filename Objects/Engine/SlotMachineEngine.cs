namespace SlotMachine.Objects.Engine
{
    using SlotMachine.Interfaces;
    using SlotMachine.Utils;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed class SlotMachineEngine : ISlotMachineEngine
    {
        private const string INITIAL_DEPOSIT_MONEY_TITLE = "Please deposit money you would like to play with:";
        private const string STAKE_AMOUNT_TITLE = "Enter stake amount:";
        private const string PROFIT_TITLE = "You have won: ";
        private const string CURRENT_BALANCE_TITLE = "Current balance is: ";
        private const string END_GAME_TITLE = "Game Over!";
        private const string INVALID_STAKE_AMOUNT_TITLE = "Please enter stake amount less or equal to your balance!";
        private const int MATRIX_ROWS_LENGTH = 4;
        private const int MATRIX_COLUMNS_LENGHT = 3;

        private static readonly SlotMachineEngine SingleInstance = new SlotMachineEngine();
        private static Random random = new Random();

        private readonly ISymbolFactory symbolFactory;
        private List<char> symbols;
        private Player player;

        private SlotMachineEngine()
        {
            this.symbolFactory = new SymbolFactory();
            this.symbols = this.symbolFactory.GetSymbols();
            this.player = new Player();
        }

        public static SlotMachineEngine Instance
        {
            get
            {
                return SingleInstance;
            }
        }

        public void Start()
        {
            this.player.Balance = this.ReadAmount(INITIAL_DEPOSIT_MONEY_TITLE);
            this.Play();
        }

        private decimal ReadAmount(string titleToDisplay)
        {
            decimal amount = 0;

            string currentLine = string.Empty;
            do
            {
                Console.WriteLine(titleToDisplay);
                currentLine = Console.ReadLine();
                decimal.TryParse(currentLine, out amount);
            } while (!string.IsNullOrEmpty(currentLine) && amount <= 0);

            return amount;
        }

        private void Play()
        {
            while (this.player.Balance > 0)
            {
                decimal stakeAmount = this.ReadAmount(STAKE_AMOUNT_TITLE);
                if (stakeAmount <= this.player.Balance)
                {
                    IReport report = this.CalculateResult(stakeAmount, this.symbols, this.player);
                    this.PrintResult(report);
                }
                else
                {
                    Console.WriteLine(INVALID_STAKE_AMOUNT_TITLE);
                }
            }

            Console.WriteLine(END_GAME_TITLE);
            Console.Read();
        }

        private IReport CalculateResult(decimal stakeAmount, List<char> symbols, Player player)
        {
            //Generate matrix
            char[,] matrix = new char[MATRIX_ROWS_LENGTH, MATRIX_COLUMNS_LENGHT];
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                symbols.Shuffle();
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    matrix[i, j] = symbols[random.Next(0, symbols.Count)];
                }
            }

            decimal totalCoefficient = 0;
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                decimal currentCoefficient = 0;
                int equalSymbolsCount = 1;
                int wildcardsCount = 0;
                for (int j = 0; j <= matrix.GetUpperBound(1) - 1; j++)
                {
                    if(matrix[i, j] == this.symbolFactory.WildcardSymbol.Name)
                    {
                        wildcardsCount++;
                    }

                    if (this.CheckIfIsLost(matrix, i, j, wildcardsCount))
                    {
                        break;
                    }
                    else
                    {
                        equalSymbolsCount++;
                        currentCoefficient = currentCoefficient + this.symbolFactory.Symbols[matrix[i, j]].Coefficient;
                    }
                }

                if (equalSymbolsCount == matrix.GetUpperBound(1) + 1)
                {
                    // Add the last coefficient from current row
                    currentCoefficient += this.symbolFactory.Symbols[matrix[i, matrix.GetUpperBound(1)]].Coefficient;

                    totalCoefficient += currentCoefficient;
                }
            }

            decimal profit = stakeAmount * totalCoefficient;

            player.Balance = player.Balance - stakeAmount + profit;

            return new Report()
            {
                Balance = player.Balance,
                Matrix = matrix,
                Profit = profit
            };
        }

        private bool CheckIfIsLost(char[,] matrix, int i, int j, int wildcardsCount)
        {
             bool isLost = matrix[i, j] != matrix[i, j + 1] &&
                        (matrix[i, j] != this.symbolFactory.WildcardSymbol.Name &&
                        matrix[i, j + 1] != this.symbolFactory.WildcardSymbol.Name);

            // check if wildcard symbol is in the middle column 
            if(j == 1 && matrix[i, j] == this.symbolFactory.WildcardSymbol.Name
                && matrix[i, j + 1] != this.symbolFactory.WildcardSymbol.Name
                && matrix[i, j - 1] != matrix[i, j + 1])
            {
                isLost = true;
            }

            if(wildcardsCount > 1)
            {
                isLost = false;
            }

            return isLost;
        }

        private void PrintResult(IReport report)
        {
            var output = new StringBuilder();

            output.AppendLine();
            for (int i = 0; i <= report.Matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= report.Matrix.GetUpperBound(1); j++)
                {
                    output.Append(report.Matrix[i, j]);
                }
                output.AppendLine();
            }
            output.AppendLine();

            output.Append(PROFIT_TITLE);
            output.AppendLine(report.Profit.ToString());
            output.Append(CURRENT_BALANCE_TITLE);
            output.AppendLine(report.Balance.ToString());

            Console.WriteLine(output.ToString());
        }
    }
}
