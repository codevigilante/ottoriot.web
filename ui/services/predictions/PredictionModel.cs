using System;
using Newtonsoft.Json.Linq;
namespace ui.services.predictions
{
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
        //public double BigGameProb { get; set; }
    }

	public class PredictionModel
	{
		public int Season { get; set; }
        public int Week { get; set; }
        public DateTime Updated { get; set; }
        public List<PredictionData> Projections { get; set; } = new List<PredictionData>();

        public List<PredictionData> Sorted()
        {
            return (Projections.OrderByDescending(p => p.FP).ToList());
        }
    }
}

