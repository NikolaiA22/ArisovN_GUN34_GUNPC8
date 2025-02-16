using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Game
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }

        private Player player;
        private Casino casino;

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в казино!");

            player = LoadPlayerProfile();

            string gameChoice = ChooseGame();

            decimal bet = PlaceBet();

            PlayGame(gameChoice, bet);

            Console.WriteLine($"Спасибо за игру, {player.Name}! До новых встреч!");

            SavePlayerProfile(player);

            Console.WriteLine("Выход из игры...");
        }

        private Player LoadPlayerProfile()
        {
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            return new Player(name);
        }

        private string ChooseGame()
        {
            Console.WriteLine("Выберите игру: 1. Покер 2. Блэкджек 3. Кости");
            string choice = Console.ReadLine();
            return choice;
        }

        private decimal PlaceBet()
        {
            decimal bet;
            do
            {
                Console.Write("Введите вашу ставку: ");
                bet = Convert.ToDecimal(Console.ReadLine());
            } while (bet > player.Bankroll);

            return bet;
        }

        private void PlayGame(string gameChoice, decimal bet)
        {
            bool playerWon = false;
                                    
            if (playerWon)
            {
                player.Bankroll += bet;
                if (player.Bankroll > player.MaxBankroll)
                {
                    Console.WriteLine("Вы разорили казино! На вашем месте построят новое!");
                    player.Bankroll = player.MaxBankroll;
                }
                Console.WriteLine("Вы выиграли!");
            }
            else
            {
                player.Bankroll -= bet;
                Console.WriteLine("Вы проиграли!");
            }
        }

        private void SavePlayerProfile(Player player)
        {
            Console.WriteLine($"Профиль игрока {player.Name} сохранен.");
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public decimal Bankroll { get; set; }
        public decimal MaxBankroll { get; private set; }

        public Player(string name)
        {
            Name = name;
            Bankroll = 100;
            MaxBankroll = 1000;
        }
    }
}
