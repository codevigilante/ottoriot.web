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

    public class StartTimes
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public int NumGames { get; set; }
        public List<string> TeamAbbrs { get; set; } = new List<string>();
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

        public List<StartTimes> FilterStartTimes()
        {
            List<StartTimes> startTimes = new List<StartTimes>();
            List<OddsData> gamesByTime = SortByDate();
            StartTimes? current = null;
            int id = 1;

            foreach(OddsData game in gamesByTime)
            {
                if (current == null || current.Start != game.GameTime)
                {
                    current = new StartTimes()
                    {
                        Id = id++,
                        Start = game.GameTime,
                        NumGames = 1,
                        TeamAbbrs = new List<string>() { game.Home, game.Away }
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

