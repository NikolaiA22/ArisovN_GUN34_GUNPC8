using PokerGame;

namespace DiceGames
{
    public struct Dice
    {
        private readonly int _minValue;
        private readonly int _maxValue;
        private static readonly Random _random = new Random();
        public int Number => _random.Next(_minValue, _maxValue + 1);
        public Dice(int min, int max)
        {
            if (min < 1 || max > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }
            if (min >= max)
            {
                throw new ArgumentException("Не может быть меньше максимального значения");
            }
            _minValue = min;
            _maxValue = max;
        }
        public int Roll()
        {
            return _random.Next(_minValue, _maxValue + 1);
        }
    }
}