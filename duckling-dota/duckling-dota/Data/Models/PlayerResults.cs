using System;
namespace duckling_dota.Data.Models
{
    public class PlayerResults
    {
        public DucklingDoter player;
        public int DuoGames;
        public int TrioGames;
        public int QuoGames;
        public int PentaGames;

        public int SumGames => DuoGames + TrioGames + QuoGames + PentaGames;
    }
}
