﻿using PokerGame;

namespace DiceGames
{
    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static readonly Random _random = new Random();
        public int Number => _random.Next(_min, _max + 1);
        public Dice(int min, int max)
        {
            if (min < 1 || max > int.MaxValue)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }
            if (min >= max)
            {
                throw new ArgumentException("Не может быть меньше максимального значения");
            }
            _min = min;
            _max = max;
        }
        public int Roll()
        {
            return _random.Next(_min, _max + 1);
        }
    }
}