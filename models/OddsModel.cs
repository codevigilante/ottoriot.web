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
	}
}

