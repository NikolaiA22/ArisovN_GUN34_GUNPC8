using PokerGame;
using DiceGames;

namespace Poker
{
    public class Casino : IGame
    { 
        private readonly List<CasinoGameBase> _games = new List<CasinoGameBase>();
        private string _playerProfile;

        public Casino()
        {
            LoadPlayerProfile();
            _games.Add(new BlackJack(5));
            _games.Add(new DiceGame(2, 1, 6));
        }
        public void StartGame()
        {
            Console.WriteLine("Выберите игру:");
            Console.WriteLine("1 - Блэкджек");
            Console.WriteLine("2 - Игра в кости");
            string choice = Console.ReadLine();

            CasinoGameBase selectedGame = null;

            switch (choice)
            {
                case "1":
                    selectedGame = _games[0];
                    break;
                case "2":
                    selectedGame = _games[1];
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    return;
            }

            SubscribeToEvents(selectedGame);
            selectedGame.PlayGame();
        }
        private void SubscribeToEvents(CasinoGameBase game)
        {
            game.OnWin += () => Console.WriteLine("Вы выиграли!");
            game.OnLoose += () => Console.WriteLine("Вы проиграли!");
            game.OnDraw += () => Console.WriteLine("Ничья!");
        }
        private void LoadPlayerProfile()
        {
            if (File.Exists("playerProfile.txt"))
            {
                _playerProfile = File.ReadAllText("playerProfile.txt");
                Console.WriteLine($"Профиль игрока загружен: {_playerProfile}");
            }
            else
            {
                Console.WriteLine("Профиль не найден, создайте новый.");
                Console.WriteLine("Введите имя игрока: ");
                _playerProfile = Console.ReadLine();
                SavePlayerProfile();
            }
        }
        private void SavePlayerProfile()
        {
            File.WriteAllText("playerPrifile.txt", _playerProfile);
            Console.WriteLine("Профиль игрока сохранен");
        }
    }
}
