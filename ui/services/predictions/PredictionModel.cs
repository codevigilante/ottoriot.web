using System;
using Newtonsoft.Json.Linq;
namespace ui.services.predictions
{
    public enum SortBy { FP, Value, Salary }
    public enum PositionFilter { ALL, QB, RB, WR, TE, DST, FLEX }
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
        public double Workload { get; set; }
        public string WorkloadTag { get; set; }
        public double TPG { get; set; }
        public double FP { get; set; }
        public string Status { get; set; }
        public double Salary { get; set; }
        public double Value { get; set; }
        public DateTime GameTime { get; set; }
    }

	public class PredictionModel
	{
		public int Season { get; set; }
        public int Week { get; set; }
        public DateTime Updated { get; set; }
        public List<AvailableSlate> Slates { get; set; } = new List<AvailableSlate>();
        public List<PredictionData> Projections { get; set; } = new List<PredictionData>();

        public List<PredictionData> Sorted()
        {
            return (Projections.OrderByDescending(p => p.FP).ToList());
        }

        public List<PredictionData> Filter(PositionFilter position)
        {
            switch(position)
            {
                case PositionFilter.QB:
                    return (Projections.Where(p => p.Position == "QB").OrderByDescending(p => p.FP).ToList());
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
                case PositionFilter.ALL:
                default:
                    return (Sorted());
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
                case SortBy.FP:
                default:
                    return (data.OrderByDescending(p => p.FP).ToList());
            }
        }
    }
}

