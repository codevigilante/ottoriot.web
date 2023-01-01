using System;
namespace models
{
	public class StartTime
	{
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public int NumGames { get; set; }
        public List<string> TeamAbbrs { get; set; } = new List<string>();
        public bool Selected { get; set; }
    }

    public class StartTimes : List<StartTime>
    {
        public int NumGamesSelected
        {
            get
            {
                return (this.Where(s => s.Selected).Sum(s => s.NumGames));
            }
        }
    }
}

