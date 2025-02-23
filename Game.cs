using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Game
    {
        //static void Main(string[] args)
        //{
        //    Game game = new Game(new FileSystemSaveLoadService("PlayerProfiles"));
        //    game.Start();
        //}

        private Player player;
        private Casino casino;
        private readonly ISaveLoadService<string> _saveLoadService;
        public Game(ISaveLoadService<string> saveLoadService)
        { 
            _saveLoadService = saveLoadService;
        }

        public void Start()
        {
            Console.WriteLine("Добро пожаловать в казино!");
            player = LoadPlayerProfile();
            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine($"Ваш текущий банк: {player.Bankroll}");
                string gameChoice = ChooseGame();

                decimal bet = PlaceBet();

                PlayGame(gameChoice, bet);

                Console.WriteLine($"Ваш текущий банк: {player.Bankroll}");

                Console.Write("Хотите сыграть еще? (да/нет): ");
                string response = Console.ReadLine()?.ToLower();
                continuePlaying = response == "да";
            }
            Console.WriteLine($"Спасибо за игру, {player.Name}! До новых встреч!");
            SavePlayerProfile(player);
            Console.WriteLine("Выход из игры...");
        }

        private Player LoadPlayerProfile()
        {
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            string identifier = name;

            try
            {
                string data = _saveLoadService.LoadData(identifier);
                string[] lines = data.Split('\n');
                if (lines.Length < 2 || !decimal.TryParse(lines[1], out decimal bankroll))
                {
                    throw new FormatException("Ошибка в формате данных профиля.");
                }
                return new Player(name, bankroll);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Профиль не найден, создается новый.");
                return new Player(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке профиля: {ex.Message}");
                return new Player(name);
            }
        }

        private string ChooseGame()
        {
            Console.WriteLine("Выберите игру: 1. Покер 2. Блэкджек 3. Кости");
            return Console.ReadLine();
        }

        private decimal PlaceBet()
        {
            decimal bet;
            while (true)
            {
                Console.Write("Введите вашу ставку: ");
                if (decimal.TryParse(Console.ReadLine(), out bet) && bet > 0 && bet <= player.Bankroll)
                {
                    break;
                }
                Console.WriteLine("Некорректная ставка. Пожалуйста, попробуйте снова.");
            }

            return bet;
        }

        private void PlayGame(string gameChoice, decimal bet)
        {
            bool playerWon = new Random().Next(0, 2) == 1;
                                    
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
            string data = $"{player.Name}\n{player.Bankroll}";
            _saveLoadService.SaveData(data, player.Name);
            Console.WriteLine($"Профиль игрока {player.Name} сохранен.");
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public decimal Bankroll { get; set; }
        public decimal MaxBankroll { get; private set; }

        public Player(string name, decimal initialBankroll = 100)
        {
            Name = name;
            Bankroll = initialBankroll;
            MaxBankroll = 1000;
        }
    }
}
