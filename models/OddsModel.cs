using System;

namespace models
{
    public class OddsData
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public double HomeSpread { get; set; }
        public double AwaySpread { get; set; }
        public double HomeITT { get; set; }
        public double AwayITT { get; set; }
        public double OverUnder { get; set; }
        public DateTime GameTime { get; set; }
    }

	public class OddsModel
	{
        public int Season { get; set; }
        public int Week { get; set; }
        public int NumGames { get; set; }
        public DateTime Updated { get; set; }        
        public List<OddsData> Games { get; set; } = new List<OddsData>();

        public OddsModel()
		{
		}

        public List<OddsData> SortByDate()
        {
            return (Games.OrderBy(g => g.GameTime).ToList());
        }

        public StartTimes FilterStartTimes(bool excludePastGames = true)
        {
            StartTimes startTimes = new StartTimes();
            List<OddsData> gamesByTime = SortByDate();
            StartTime? current = null;
            int id = 1;

            foreach(OddsData game in gamesByTime)
            {
                if (excludePastGames && game.GameTime < DateTime.Now)
                {
                    continue;
                }

                if (current == null || current.Start != game.GameTime)
                {
                    current = new StartTime()
                    {
                        Id = id++,
                        Start = game.GameTime,
                        NumGames = 1,
                        TeamAbbrs = new List<string>() { game.Home, game.Away },
                        Selected = true
                    };

                    startTimes.Add(current);
                }
                else
                {
                    current.NumGames += 1;
                    current.TeamAbbrs.Add(game.Home);
                    current.TeamAbbrs.Add(game.Away);
                }
            }

            return (startTimes);
        }

        public OddsData? GetData(string teamAbbr)
        {
            OddsData? game = Games.SingleOrDefault(g => g.Home == teamAbbr || g.Away == teamAbbr);

            return (game);
        }
	}
}

