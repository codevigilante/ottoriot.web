using System;

namespace models
{
    public enum SortBy { FP, Value, Salary, Matchup }
    public enum PositionFilter { QB, RB, WR, TE, DST, FLEX }
    public enum GameDayFilter { All, Thu, Sat, Sun, Mon }
    public enum TimeSlotFilter { All, AM, Noon, PM, Late }

    public class AvailableSlate
    {
        public string Day { get; set; }
        public string Time { get; set; }
    }

	public class PredictionData
	{
        public string Name { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Vs { get; set; }
        public double FP { get; set; }
        public string Status { get; set; }
        public double Salary { get; set; }
        public double Value { get; set; }
        public DateTime GameTime { get; set; }
        public double Matchup { get; set; }
        public double BGP { get; set; }
    }

	public class PredictionModel
	{
		public int Season { get; set; }
        public int Week { get; set; }
        public DateTime Updated { get; set; }
        public List<AvailableSlate> Slates { get; set; } = new List<AvailableSlate>();
        public List<PredictionData> Projections { get; set; } = new List<PredictionData>();

        public List<PredictionData> Filter(PositionFilter position)
        {
            switch(position)
            {                    
                case PositionFilter.RB:
                    return (Projections.Where(p => p.Position == "RB").OrderByDescending(p => p.FP).ToList());
                case PositionFilter.WR:
                    return (Projections.Where(p => p.Position == "WR").OrderByDescending(p => p.FP).ToList());
                case PositionFilter.TE:
                    return (Projections.Where(p => p.Position == "TE").OrderByDescending(p => p.FP).ToList());
                case PositionFilter.DST:
                    return (Projections.Where(p => p.Position == "DST").OrderByDescending(p => p.FP).ToList());
                case PositionFilter.FLEX:
                    return (Projections.Where(p => p.Position == "RB" || p.Position == "WR" || p.Position == "TE").OrderByDescending(p => p.FP).ToList());
                case PositionFilter.QB:
                default:
                    return (Projections.Where(p => p.Position == "QB").OrderByDescending(p => p.FP).ToList());
            }
        }

        public List<PredictionData> Filter(PositionFilter position, SortBy sort)
        {
            List<PredictionData> data = Filter(position);

            switch (sort)
            {
                case SortBy.Salary:
                    return (data.OrderByDescending(p => p.Salary).ToList());
                case SortBy.Value:
                    return (data.OrderByDescending(p => p.Value).ToList());
                case SortBy.Matchup:
                    return (data.OrderByDescending(p => p.Matchup).ThenByDescending(p => p.FP).ToList());
                case SortBy.FP:
                default:
                    return (data.OrderByDescending(p => p.FP).ToList());
            }
        }

        public List<PredictionData> FilterOnlyPlayed(PositionFilter position, SortBy sort)
        {
            List<PredictionData> played = Filter(position, sort);

            played = played.Where(p => p.GameTime >= DateTime.Now).ToList();

            return (played);
        }

        public List<PredictionData> FilterByStartTimes(List<StartTimes> startTimes, PositionFilter position, SortBy sort = SortBy.FP)
        {
            List<PredictionData> data = Filter(position, sort);

            data = data.Where(d => startTimes.SingleOrDefault(t => t.Start == d.GameTime) != null).ToList();

            return (data);
        }
    }
}

