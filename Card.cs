using System;

namespace PokerGame
{
    public enum Suit
    { 
        Diamonds,
        Hearts,
        Clubs,
        Spades
    }
    public enum Rank
    { 
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public struct Card
    { 
        public Suit Suit { get; }
        public Rank Rank { get; }
        public Card(Suit suit, Rank rank)
        { 
            Suit = suit;
            Rank = rank;
        }
        public int GetValue()
        { 
            if(Rank >= Rank.Ten)
                return 10;
            return (int)Rank;
        }
    }
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int min, int max)
            : base($"Некорректное число: {number}. Оно должно находиться между {min} и {max}.")
        { 
            
        }
    }
}
