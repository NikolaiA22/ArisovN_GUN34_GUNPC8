using PokerGame;
using DiceGames;

namespace Poker
{
    public class Casino : IGame
    { 
        private readonly List<CasinoGameBase> _games = new List<CasinoGameBase>();
        private string _playerProfile;
        private string _playerName;
        private decimal _playerBankroll;

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
                try
                {
                    _playerProfile = File.ReadAllText("playerProfile.txt");
                    Console.WriteLine($"Профиль игрока загружен: {_playerProfile}");
                    Console.WriteLine($"Ваш текущий банк: {_playerBankroll}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке профиля: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Профиль не найден, создайте новый.");
                Console.WriteLine("Введите имя игрока: ");
                _playerName = Console.ReadLine();
                _playerBankroll = 100;
                SavePlayerProfile();
            }
        }
        private void SavePlayerProfile()
        {
            try
            {
                File.WriteAllLines("playerProfile.txt", new string[] { _playerName, _playerBankroll.ToString()});
                Console.WriteLine("Профиль игрока сохранен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении профиля: {ex.Message}");
            }
        }
    }
}
