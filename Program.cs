using Poker;
using PokerGame;
using DiceGames;
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game(new FileSystemSaveLoadService("PlayerProfiles"));
        game.Start();
    }
}



