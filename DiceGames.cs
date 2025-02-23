using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerGame;

namespace DiceGames
{
    public class DiceGame : CasinoGameBase
    { 
        private readonly List<Dice> _diceCollection = new List<Dice>();
        private readonly int _numberOfDice;
        private readonly int _minValue;
        private readonly int _maxValue;

        public DiceGame(int numberOfDice, int minValue, int maxValue)
        {
            if (numberOfDice < 1)
                throw new ArgumentException("Число на кубике не может быть меньше 1.");
            if (minValue < 1 || maxValue < minValue)
                throw new ArgumentException("Некорректное минимальное или максимальное значение.");
            _numberOfDice = numberOfDice;
            _minValue = minValue;
            _maxValue = maxValue;

            FactoryMethod();
        }
        protected override void FactoryMethod()
        {
            for (int i = 0; i < _numberOfDice; i++)
            {
                _diceCollection.Add(CreateDiceInstance(_minValue, _maxValue));
            }
        }
        protected virtual Dice CreateDiceInstance(int min, int max)
        {
            return new Dice(min, max);
        }

        public override void PlayGame()
        {
            List<int> playerRolls = RollDice(out int playerScore);
            List<int> computerRolls = RollDice(out int computerScore);

            Console.WriteLine("Значения кубиков игрока: " + string.Join(", ", playerRolls));
            Console.WriteLine("Значения кубиков компьютера: " + string.Join(", ", computerRolls));

            Console.WriteLine($"Счет игрока: {playerScore}");
            Console.WriteLine($"Счет компьютера: {computerScore}");

            DetermineOutcome(playerScore, computerScore);
        }
        private List<int> RollDice(out int totalScore)
    {
        totalScore = 0;
        List<int> rolls = new List<int>();

        foreach (var dice in _diceCollection)
        {
            int roll = dice.Roll();
            rolls.Add(roll);
            totalScore += roll;
        }
        return rolls;
    }
        private void DetermineOutcome(int playerScore, int computerScore)
        {
            if (playerScore > computerScore)
            {
                OnWinInvoke();
                Console.WriteLine("Player Win!");
            }
            else if (playerScore < computerScore)
            {
                OnLooseInvoke();
                Console.WriteLine("Computer Win!");
            }
            else
            {
                OnDrawInvoke();
                Console.WriteLine("Ничья!");
            }
        }
    }
}
