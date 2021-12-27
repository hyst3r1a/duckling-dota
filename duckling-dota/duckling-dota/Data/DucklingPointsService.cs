using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using duckling_dota.Data.Interfaces;
using duckling_dota.Data.Models;
using static System.Net.WebRequestMethods;

namespace duckling_dota.Data
{
    public class DucklingPointsService
    {
        public static string TierDate
            {
            get
            {
                string result = string.Empty;
                if(DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 1, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 4, 1))
                {
                    result = $"{DateTime.UtcNow.Date.Year}/1";
                }
                if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 4, 2) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 6, 30))
                {
                    result = $"{DateTime.UtcNow.Date.Year}/2";
                }
                if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 7, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 9, 30))
                {
                    result = $"{DateTime.UtcNow.Date.Year}/3";
                }
                if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 10, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 12, 31))
                {
                    result = $"{DateTime.UtcNow.Date.Year}/4";
                }
                return result;
            }
            }
        public const string OfficerPassword = "duckling_friend_is_good";

        public List<string> ducklings = new List<string>();
        public List<PlayerResults> playerResults = new List<PlayerResults>();

        public List<DucklingDoter> doters = new List<DucklingDoter>();
        private readonly IDataAccessProvider _data;

        public DucklingPointsService(IDataAccessProvider data)
        {
            _data = data;
        }

        public void GetAllDoters()
        {
            doters = _data.GetDotersRecords();
        }
        public void AddDoter(DucklingDoter newDoter)
        {
            _data.AddDoterRecord(newDoter);
            GetAllDoters();
        }
        public void EditDoter(DucklingDoter newDoter)
        {
            _data.UpdateDoterRecord(newDoter);
            GetAllDoters();
        }
        public void RemoveDoter(DucklingDoter newDoter)
        {
            _data.DeleteDoterRecord(newDoter.Id);
            GetAllDoters();
        }

        private int DaysOffset()
        {
            int result = 0;
            if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 1, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 4, 1))
            {
                result = DateTime.UtcNow.Date.DayOfYear - new DateTime(DateTime.UtcNow.Date.Year, 1, 1).DayOfYear;
            }
            if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 4, 2) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 6, 30))
            {
                result = DateTime.UtcNow.Date.DayOfYear - new DateTime(DateTime.UtcNow.Date.Year, 4, 2).DayOfYear;
            }
            if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 7, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 9, 30))
            {
                result = DateTime.UtcNow.Date.DayOfYear - new DateTime(DateTime.UtcNow.Date.Year, 7, 1).DayOfYear;
            }
            if (DateTime.UtcNow.Date > new DateTime(DateTime.UtcNow.Date.Year, 10, 1) && DateTime.UtcNow.Date < new DateTime(DateTime.UtcNow.Date.Year, 12, 31))
            {
                result = DateTime.UtcNow.Date.DayOfYear - new DateTime(DateTime.UtcNow.Date.Year, 10, 1).DayOfYear;
            }
            return result;
        }

        public void CountPointsForPlayer(DucklingDoter player)
        {
            List<MatchInfo> matchInfos = new List<MatchInfo>();
            PlayerResults results = new PlayerResults();
            results.player = player;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.opendota.com/api/players/{player.DotaId}/matches?date={DaysOffset()}&included_account_id={player.DotaId}");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                string responseString = reader.ReadToEnd();
                matchInfos = JsonSerializer.Deserialize<List<MatchInfo>>(responseString, options);
            }

            foreach (var matchInfo in matchInfos)
            {
                if (matchInfo.leaver_status == 0 || matchInfo.leaver_status == 1)
                {
                    int partySize = doters.Select(x => x.DotaId).Intersect(matchInfo.heroes.Values.ToList().Select(y => y.account_id.ToString())).Count();
                    switch (partySize)
                    {
                        case 2:
                            results.DuoGames++;
                            break;
                        case 3:
                            results.TrioGames++;
                            break;
                        case 4:
                            results.QuoGames++;
                            break;
                        case 5:
                            results.PentaGames++;
                            break;
                    }
                }
            }
            playerResults.Add(results);

        }
    }
}
