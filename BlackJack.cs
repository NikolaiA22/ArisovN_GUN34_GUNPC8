using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PokerGame
{
    public class BlackJack : CasinoGameBase
    { 
        private readonly Queue<Card> _deck = new Queue<Card>();
        private readonly int _numberOfCards;

        public BlackJack(int numberOfCards)
        {
            if (numberOfCards < 1 || numberOfCards > 52)
                throw new ArgumentException("Число карт должно находиться между 1 и 52.");
            _numberOfCards = numberOfCards;
        }
        protected override void FactoryMethod()
        {
            Shuffle();
        }
        public override void PlayGame()
        {
            List<Card> playerCards = DrawCards(2);
            List<Card> dealerCards = DrawCards(2);

            Console.WriteLine("Карты игрока: " + string.Join(", ", playerCards.Select(card => card.ToString())));
            Console.WriteLine("Карты дилера: " + string.Join(", ", dealerCards.Select(card => card.ToString())));

            int playerScore = CalculateScore(playerCards);
            int dealerScore = CalculateScore(dealerCards);

            Console.WriteLine($"Очки игрока: {playerScore}");
            Console.WriteLine($"Очки дилера: {dealerScore}");

            DetermineOutcome(playerScore, dealerScore);
        }

        private List<Card> DrawCards(int count)
        { 
            List<Card> cards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                if (_deck.Count == 0) throw new InvalidOperationException("В колоде нет карт.");
                cards.Add(_deck.Dequeue());
            }
            return cards;
        }
        private int CalculateScore(List<Card> cards)
        { 
            int score = 0;
            int acesCount = 0;
            foreach (var card in cards)
            {
                score += card.GetValue();
                if(card.Rank == Rank.Ace) acesCount++;
            }
            while (score > 21 && acesCount > 0)
            {
                score -= 10;
                acesCount--;
            }
            return score;
        }
        private void DetermineOutcome(int playerScore, int dealerScore)
        {
            if (playerScore > 21)
            {
                OnLooseInvoke();
                Console.WriteLine("Игрок проиграл! Дилер победил.");
            }
            else if (dealerScore > 21)
            {
                OnWinInvoke();
                Console.WriteLine("Дилер проиграл! Игрок победил.");
            }
            else if (playerScore > dealerScore)
            {
                OnWinInvoke();
                Console.WriteLine("Игрок победил!");
            }
            else if (dealerScore > playerScore)
            {
                OnLooseInvoke();
                Console.WriteLine("Дилер победил!");
            }
            else
            { 
                OnDrawInvoke();
                Console.WriteLine("Сбросил!");
            }
        }
        private void Shuffle()
        { 
            List<Card> cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
            Random random = new Random();
            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count);
                Card temp = cards[j];
                cards[i] = cards[j];
                cards[j] = temp;
            }
            foreach (var card in cards)
            { 
                _deck.Enqueue(card);
            }
        }
    }
}
