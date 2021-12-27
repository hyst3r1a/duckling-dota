using System;
using System.Collections.Generic;
using duckling_dota.Data.Models.OpenDotaHeroContainer;

namespace duckling_dota.Data.Models
{
    public class MatchInfo
    {
        public long match_id { get; set; }
        public int player_slot { get; set; }
        public bool radiant_win { get; set; }
        public int start_time { get; set; }
        public Dictionary<int, OpenDotaHero> heroes { get; set; }
        public int duration { get; set; }
        public int game_mode { get; set; }
        public int lobby_type { get; set; }
        public int hero_id { get; set; }
        public object version { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public object skill { get; set; }
        public int leaver_status { get; set; }
        public int party_size { get; set; }
    }
}
