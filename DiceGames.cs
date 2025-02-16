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
        }
        protected override void FactoryMethod()
        {
            for (int i = 0; i < _numberOfDice; i++)
            {
                _diceCollection.Add(new Dice(_minValue, _maxValue));
            }
        }

        public override void PlayGame()
        {
            int playerScore = RollDice();
            int computerScore = RollDice();

            Console.WriteLine($"Счет игрока: {playerScore}");
            Console.WriteLine($"Счет компьютера: {computerScore}");

            DetermineOutcome(playerScore, computerScore);
        }
        private int RollDice()
        {
            int totalScore = 0;
            foreach (var dice in _diceCollection)
            {
                totalScore += dice.Roll();
            }
            return totalScore;
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
