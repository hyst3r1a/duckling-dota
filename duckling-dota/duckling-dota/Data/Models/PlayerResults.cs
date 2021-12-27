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

        public float SumGames => DuoGames * 0.01f + TrioGames * 0.01f + QuoGames * 0.015f + PentaGames * 0.02f;
    }
}
